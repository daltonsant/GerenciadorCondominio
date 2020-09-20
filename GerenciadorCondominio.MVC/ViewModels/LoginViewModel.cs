using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.MVC.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="The field {0} is required")]
        [EmailAddress(ErrorMessage ="Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
