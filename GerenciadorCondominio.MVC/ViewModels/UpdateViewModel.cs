using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.MVC.ViewModels
{
    public class UpdateViewModel
    {
        public string UserId { get; set; }
        
        [Required(ErrorMessage ="The field {0} is required")]
        [StringLength(40, ErrorMessage ="User less characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Phone { get; set; }

        public string Photo { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(40, ErrorMessage = "User less characters")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }

    }
}
