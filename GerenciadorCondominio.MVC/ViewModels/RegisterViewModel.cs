using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.MVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="The field {0} is required")]
        [StringLength(40, ErrorMessage ="Use less characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required")]
        public string Cpf { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required")]
        public string Phone { get; set; }

        public string Photo { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(40, ErrorMessage = "Use less characters")]
        [EmailAddress(ErrorMessage ="Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(40, ErrorMessage = "Use less characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(40, ErrorMessage = "Use less characters")]
        [DataType(DataType.Password)]
        [Display(Name="Confirm password")]
        [Compare("Password",ErrorMessage ="The passwords should match")]
        public string ConfirmPassword { get; set; }

    }
}
