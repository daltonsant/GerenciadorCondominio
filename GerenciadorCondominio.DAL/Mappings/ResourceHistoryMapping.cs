using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mappings
{
    public class ResourceHistoryMapping : IEntityTypeConfiguration<ResourceHistory>
    {
        public void Configure(EntityTypeBuilder<ResourceHistory> builder)
        {
            builder.HasKey(x => x.ResourceHistoryId);
            builder.Property(x => x.Value).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.MonthId).IsRequired();
            builder.Property(x => x.Day).IsRequired();
            builder.Property(x => x.Year).IsRequired();

            builder.HasOne(x => x.Month).WithMany(x => x.ResourcesHistories).HasForeignKey(x => x.MonthId);

            builder.ToTable("ResourcesHistories");
        }
    }
}
