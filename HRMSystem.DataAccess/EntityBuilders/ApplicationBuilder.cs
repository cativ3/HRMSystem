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
    public class ApplicationBuilder : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(50);

            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50);

            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(128);

            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(25);

            builder.Property(x => x.About).IsRequired();
            builder.Property(x => x.About).HasMaxLength(2000);

            builder.Property(x => x.AppliedDate).IsRequired();


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


            builder.ToTable("Applications");
        }
    }
}
