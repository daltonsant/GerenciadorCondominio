using System;

namespace GerenciadorCondominio.BLL.Models
{
    public class EdificeService
    {
        public int EdificeServiceId { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
        public DateTime? ExecutionDate { get; set; }
    }
}