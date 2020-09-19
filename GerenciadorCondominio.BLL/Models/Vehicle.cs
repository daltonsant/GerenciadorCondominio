using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Vehicle
    {
        
        public int VehicleId { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(20,ErrorMessage ="Use less characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(20, ErrorMessage = "Use less characters")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(20, ErrorMessage = "Use less characters")]
        public string Color { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")] 
        public string LicensePlate { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
