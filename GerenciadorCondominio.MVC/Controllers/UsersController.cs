using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interfaces;
using GerenciadorCondominio.MVC.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCondominio.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRoleRepository _roleRepository;


        public UsersController(IUserRepository userRepository, IRoleRepository roleRepository, IWebHostEnvironment webHostEnvironment)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userRepository.FindAll());
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if(photo != null)
                {
                    var imageFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string photoName = Guid.NewGuid().ToString() + photo.FileName;
                    using(FileStream fileStream = new FileStream(Path.Combine(imageFolder, photoName), FileMode.Create))
                    {
                        await photo.CopyToAsync(fileStream);
                        viewModel.Photo = "~/Images/" + photoName;
                    }
                }
                
                User user = new User();
                IdentityResult userCreated;

                if(_userRepository.VerifyIfExists() == 0)
                {
                    user.UserName = viewModel.Name;
                    user.Cpf = viewModel.Cpf;
                    user.Email = viewModel.Email;
                    user.PhoneNumber = viewModel.Phone;
                    user.Photo = viewModel.Photo;
                    user.IsFirstAccess = false;
                    user.Status = AccountStatus.Approved;

                    userCreated = await _userRepository.CreateUser(user, viewModel.Password);

                    if (userCreated.Succeeded)
                    {
                        await _userRepository.IncludeUserInRole(user, "Administrator");
                        await _userRepository.UserLogin(user, false);
                        return RedirectToAction("Index", "Users");
                    }

                }

                user.UserName = viewModel.Name;
                user.Cpf = viewModel.Cpf;
                user.Email = viewModel.Email;
                user.PhoneNumber = viewModel.Phone;
                user.Photo = viewModel.Photo;
                user.IsFirstAccess = false;
                user.Status = AccountStatus.Analyzing;

                userCreated = await _userRepository.CreateUser(user, viewModel.Password);

                if (userCreated.Succeeded)
                {
                    return View("Analysing", user.UserName);
                }
                else
                {
                    foreach(IdentityError error in userCreated.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(viewModel);
                }
            }
            return View(viewModel);
        }


        public IActionResult Analysing(string Name)
        {
            return View(Name);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (!User.Identity.IsAuthenticated)
                await _userRepository.Logout();

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userRepository.GetUserbyEmail(model.Email);
                if (user != null)
                {
                    if (user.Status == AccountStatus.Analyzing)
                    {
                        return View("Analysing", user.UserName);
                    }
                    else if (user.Status == AccountStatus.Reproved)
                    {
                        return View("Reproved", user.UserName);
                    }
                    else if (user.IsFirstAccess == true)
                    {
                        return View("PasswordRedefinition", user);
                    }
                    else
                    {
                        PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

                        if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) != PasswordVerificationResult.Failed)
                        {
                            await _userRepository.UserLogin(user, false);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "User and/or password incorrect");
                            return View(model);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User and/or password incorrect");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _userRepository.Logout();
            return RedirectToAction("Login");
        }

       public async Task<JsonResult> ApproveUser(string userId)
        {
            User user = await _userRepository.FindById(userId);
            user.Status = AccountStatus.Approved;
            await _userRepository.IncludeUserInRole(user, "Resident");
            await _userRepository.Update(user);

            return Json(true);
        }

        public async Task<JsonResult> ReproveUser(string userId)
        {
            User user = await _userRepository.FindById(userId);
            user.Status = AccountStatus.Reproved;
            await _userRepository.Update(user);

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> ManageUser(string userId, string name)
        {
            if(userId == null)
                return NotFound();
            TempData["userId"] = userId;
            ViewBag.Name = name;
            User user = await _userRepository.FindById(userId);
            if (user == null)
                return NotFound();
            List<UserRoleViewModel> viewModel = new List<UserRoleViewModel>();
            foreach(Role role in await _roleRepository.FindAll())
            {
                UserRoleViewModel model = new UserRoleViewModel
                {
                    RoleId = role.Id,
                    Name = role.Name,
                    Description = role.Description
                };
                
                if(await _userRepository.VerifyIfUserIsInRole(user, role.Name))
                    model.IsSelected = true;
                else
                    model.IsSelected = false;

                viewModel.Add(model);
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUser(List<UserRoleViewModel> model)
        {
            string userId = TempData["userId"].ToString();
            User user = await _userRepository.FindById(userId);
            
            if (user == null)
                return NotFound();
            IEnumerable<string> roles = await _userRepository.GetUserRoles(user);

            IdentityResult result = await _userRepository.RemoveUserRoles(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Was not possible to update roles of the user");
                return View("ManageUser", userId);
            }

            result = await _userRepository.IncludeUserInRole(user,
                model.Where(x => x.IsSelected).Select(x => x.Name));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Was not possible to update roles of the user");
                return View("ManageUser", userId);
            }

            return RedirectToAction(nameof(Index));
        } 

        public async Task<IActionResult> MyInformations()
        {
            return View(await _userRepository.FindUserByName(User));
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            User user = await _userRepository.FindById(id);
            if (user == null)
                return NotFound();

            UpdateViewModel model = new UpdateViewModel()
            {
                UserId = user.Id,
                Name = user.UserName,
                Cpf = user.Cpf,
                Email = user.Email,
                Photo = user.Photo,
                Phone = user.PhoneNumber
            };
            TempData["Photo"] = user.Photo;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateViewModel viewModel, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null)
                {
                    var imageFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    string photoName = Guid.NewGuid().ToString() + photo.FileName;
                    using (FileStream fileStream = new FileStream(Path.Combine(imageFolder, photoName), FileMode.Create))
                    {
                        await photo.CopyToAsync(fileStream);
                        viewModel.Photo = "~/Images/" + photoName;
                    }
                }
                else
                {
                    viewModel.Photo = TempData["photo"].ToString();
                }
                User user = await _userRepository.FindById(viewModel.UserId);
                user.UserName = viewModel.Name;
                user.Cpf = viewModel.Cpf;
                user.PhoneNumber = viewModel.Phone;
                user.Photo = viewModel.Photo;
                user.Email = viewModel.Email;

                await _userRepository.Update(user);
                if(await _userRepository.VerifyIfUserIsInRole(user, "Administrator") ||
                    await _userRepository.VerifyIfUserIsInRole(user, "Syndic"))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(MyInformations));
                }
            }

            return View(viewModel);
        }
    }
}
