using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominio.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly Context _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _loginManager;

        public UserRepository(Context context, UserManager<User> userManager, SignInManager<User> loginManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _loginManager = loginManager;
        }

        public async Task<IdentityResult> CreateUser(User user, string password)
        {
            try
            {
                return await _userManager.CreateAsync(user, password);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<User> GetUserbyEmail(string email)
        {
            try
            {
                return await _userManager.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task IncludeUserInRole(User user, string role)
        {
            try
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Logout()
        {
            try
            {
                await _loginManager.SignOutAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task UserLogin(User user, bool remember)
        {
            try
            {
                await _loginManager.SignInAsync(user, remember);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int VerifyIfExists()
        {
            try
            {
                return _context.Users.Count();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
