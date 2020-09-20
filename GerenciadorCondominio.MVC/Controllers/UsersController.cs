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

        public UsersController(IUserRepository userRepository, IWebHostEnvironment webHostEnvironment)
        {
            _userRepository = userRepository;
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

    }
}
