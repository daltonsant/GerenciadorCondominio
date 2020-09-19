using GerenciadorCondominio.BLL.Models;
using GerenciadorCondominio.DAL.Mappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL
{
    public class Context : IdentityDbContext<User,Role, string>
    {
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Event> Events { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public DbSet<ResourceHistory> ResourceHitories { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<EdificeService> EdificeServices { get; set; }
        public override  DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RentMapping());
            builder.ApplyConfiguration(new ApartmentMapping());

            builder.ApplyConfiguration(new EventMapping());

            builder.ApplyConfiguration(new RoleMapping());
            builder.ApplyConfiguration(new ResourceHistoryMapping());
            builder.ApplyConfiguration(new MonthMapping());
            builder.ApplyConfiguration(new ServiceMapping());
            builder.ApplyConfiguration(new EdificeServiceMapping());
            builder.ApplyConfiguration(new UserMapping());
            builder.ApplyConfiguration(new VehicleMapping());

        }
    }
}
