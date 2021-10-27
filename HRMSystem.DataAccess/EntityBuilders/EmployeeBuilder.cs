using HRMSystem.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.DataAccess.EntityBuilders
{
    public class EmployeeBuilder : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(50);

            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50);

            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50);

            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(50);

            builder.Property(x => x.Salary).IsRequired();
            builder.Property(x => x.Salary).HasColumnType("decimal(18, 2)");

            builder.Property(x => x.WorkingStatus).IsRequired();
            builder.Property(x => x.WorkingStatus).HasConversion<int>();

            builder.Property(x => x.StartingDate).IsRequired();

            builder.Property(x => x.EndingDate).IsRequired(false);

            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();


            // Relationships

            builder
                .HasOne<City>(x => x.City)
                .WithMany()
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne<Country>(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne<WorkTitle>(x => x.WorkTitle)
                .WithMany()
                .HasForeignKey(x => x.WorkTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            builder.ToTable("Employees");
        }
    }
}
