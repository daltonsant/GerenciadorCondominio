using GerenciadorCondominio.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominio.DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        int VerifyIfExists();
        Task UserLogin(User user, bool remember);
        Task<IdentityResult> CreateUser(User user, string password);
        Task IncludeUserInRole(User user, string role);

        Task<User> GetUserbyEmail(string email);
        Task Logout();
    }
}
