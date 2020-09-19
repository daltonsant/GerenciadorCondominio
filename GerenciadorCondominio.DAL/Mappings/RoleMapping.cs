using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mappings
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(f => f.Id).ValueGeneratedOnAdd();
            builder.Property(f => f.Description).IsRequired().HasMaxLength(30);

            builder.HasData(
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Resident",
                    NormalizedName = "RESIDENT",
                    Description = "building resident"
                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "syndic",
                    NormalizedName = "SYNDIC",
                    Description = "building SYNDIC"
                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Description = "building Administrator"
                });

            builder.ToTable("Roles");
        }
    }
}
