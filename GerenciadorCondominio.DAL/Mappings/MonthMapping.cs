using GerenciadorCondominio.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.DAL.Mappings
{
    public class MonthMapping : IEntityTypeConfiguration<Month>
    {
        public void Configure(EntityTypeBuilder<Month> builder)
        {
            builder.HasKey(f => f.MonthId);
            builder.Property(f => f.MonthId).ValueGeneratedNever();
            builder.Property(f => f.Name).IsRequired().HasMaxLength(30);
            builder.HasIndex(f => f.MonthId).IsUnique();

            builder.HasMany(f => f.Rents).WithOne(f => f.Month);
            builder.HasMany(f => f.ResourcesHistories).WithOne(f => f.Month);

            builder.HasData(
                new Month { 
                    MonthId = 1,
                    Name = "January"
                },
                new Month
                {
                    MonthId = 2,
                    Name = "February"
                },
                new Month
                {
                    MonthId = 3,
                    Name = "March"
                },
                new Month
                {
                    MonthId = 4,
                    Name = "April"
                },
                new Month
                {
                    MonthId = 5,
                    Name = "May"
                },
                new Month
                {
                    MonthId = 6,
                    Name = "June"
                },
                new Month
                {
                    MonthId = 7,
                    Name = "July"
                },
                new Month
                {
                    MonthId = 8,
                    Name = "August"
                },
                new Month
                {
                    MonthId = 9,
                    Name = "September"
                },
                new Month
                {
                    MonthId = 10,
                    Name = "October"
                },
                new Month
                {
                    MonthId = 11,
                    Name = "November"
                },
                new Month
                {
                    MonthId = 12,
                    Name = "December"
                });

            builder.ToTable("Months");
        }
    }
}
