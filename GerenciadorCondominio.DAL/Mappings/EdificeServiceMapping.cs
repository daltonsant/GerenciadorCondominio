using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mappings
{
    public class EdificeServiceMapping : IEntityTypeConfiguration<EdificeService>
    {
        public void Configure(EntityTypeBuilder<EdificeService> builder)
        {
            builder.HasKey(x => x.EdificeServiceId);
            builder.Property(x => x.ServiceId).IsRequired();
            builder.Property(x => x.ExecutionDate).IsRequired();


            builder.HasOne(x => x.Service).WithMany(x => x.EdificeServices).HasForeignKey(x => x.ServiceId);
            builder.ToTable("EdificeServices");
        }
    }
}
