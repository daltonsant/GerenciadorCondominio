using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Apartment
    {
        public int ApartmentId { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid Value")]
        [Display(Name = "Number")]
        public int Number { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Range(0, 60, ErrorMessage = "Invalid Value")]
        public int Floor { get; set; }

        public string Photo { get; set; }
        public string ResidentId { get; set; }
        public virtual User Resident { get; set; }

        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }

    }
}
