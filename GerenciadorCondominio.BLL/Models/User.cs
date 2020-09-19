using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class User : IdentityUser<string>
    {
        public string Cpf { get; set; }

        public string Photo { get; set; }

        public bool IsFirstAccess { get; set; }

        public AccountStatus Status { get; set; }

        public virtual ICollection<Apartment> ApartmentsResidents { get; set; }

        public virtual ICollection<Apartment> ApartmentsOwners { get; set; }

        public virtual ICollection<Vehicle>  Vehicles { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Service> Services { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }

    public enum AccountStatus
    {
        Analyzing, Approved, Reproved
    }
}
