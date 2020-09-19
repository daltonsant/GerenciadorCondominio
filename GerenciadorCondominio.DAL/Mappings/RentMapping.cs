using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mappings
{
    public class RentMapping : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> builder)
        {
            builder.HasKey(x => x.RentId);
            builder.Property(x => x.Value).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.MonthId).IsRequired();
            builder.Property(x => x.Year).IsRequired();

            builder.HasOne(x => x.Month).WithMany(x => x.Rents).HasForeignKey(x => x.MonthId);
            builder.HasMany(x => x.Payments).WithOne(x => x.Rent);

            builder.ToTable("Rents");

        }
    }
}
