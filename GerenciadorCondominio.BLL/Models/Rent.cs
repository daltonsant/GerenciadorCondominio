using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Rent
    {
        public int RentId { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid value")]
        public decimal Value { get; set; }
        
        [Display(Name = "Month")]
        public int MonthId { get; set; }
        public Month Month { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [Range(2020, 2050, ErrorMessage = "Invalid year")]
        public int Year { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

    }
}
