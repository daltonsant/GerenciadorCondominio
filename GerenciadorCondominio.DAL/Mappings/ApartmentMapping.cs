using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mappings
{
    public class ApartmentMapping : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasKey(v => v.ApartmentId);
            builder.Property(v => v.Number).IsRequired();
            builder.Property(v => v.Floor).IsRequired();
            builder.Property(v => v.Photo).IsRequired();
            builder.Property(v => v.OwnerId).IsRequired();
            builder.Property(v => v.ResidentId).IsRequired(false);

            builder.HasOne(v => v.Owner).WithMany(v => v.ApartmentsOwners).HasForeignKey(v => v.OwnerId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(v => v.Resident).WithMany(v => v.ApartmentsResidents).HasForeignKey(v => v.ResidentId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Apartments");
        }
    }
}
