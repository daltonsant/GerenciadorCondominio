using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Month
    {
        public int MonthId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }

        public virtual ICollection<ResourceHistory> ResourcesHistories { get; set; }
    }
}
