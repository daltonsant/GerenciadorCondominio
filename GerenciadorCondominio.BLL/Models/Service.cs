using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public ServiceStatus Status { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<EdificeService> EdificeServices { get; set; }
    }

    public enum ServiceStatus
    {
        Pending, Accepted, Denied
    }
}
