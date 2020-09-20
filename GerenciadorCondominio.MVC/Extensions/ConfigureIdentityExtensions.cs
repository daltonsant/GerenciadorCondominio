using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.MVC.Extensions
{
    public static class ConfigureIdentityExtensions
    {

        public static void ConfigureNameUser(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvxwyz1234567890*-!ABCDEFGHIJKLMNOPQRSTUVWXYZ._#@+";
                options.User.RequireUniqueEmail = true;
            });
        }

        public static void ConfigureUserPassword(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 0;
            });
        }
    }
}
