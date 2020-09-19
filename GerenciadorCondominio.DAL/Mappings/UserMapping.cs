using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(f => f.Id).ValueGeneratedOnAdd();
            builder.Property(f => f.Cpf).IsRequired().HasMaxLength(30);
            builder.HasIndex(f => f.Cpf).IsUnique();

            builder.Property(f => f.Photo).IsRequired();
            builder.Property(f => f.IsFirstAccess).IsRequired();
            builder.Property(f => f.Status).IsRequired();

            builder.HasMany(f => f.ApartmentsOwners).WithOne(f => f.Owner);
            builder.HasMany(f => f.ApartmentsResidents).WithOne(f => f.Resident);
            builder.HasMany(f => f.Vehicles).WithOne(f => f.User);
            builder.HasMany(f => f.Events).WithOne(f => f.User);
            builder.HasMany(f => f.Payments).WithOne(f => f.User);
            builder.HasMany(f => f.Services).WithOne(f => f.User);

            builder.ToTable("Users");

        }
    }
}
