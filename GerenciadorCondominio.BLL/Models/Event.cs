using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Event
    {
        public int EventId { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(20, ErrorMessage = "Use less characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
