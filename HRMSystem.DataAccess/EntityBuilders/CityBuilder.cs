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
    public class CityBuilder : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(255);

            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();


            // Relationships

            builder.HasOne<Country>(x => x.Country).WithMany(x => x.Cities).HasForeignKey(x => x.CountryId);

            builder.HasMany<User>().WithOne(x => x.City).IsRequired(false);


            builder.ToTable("Cities");

            builder.HasData(new City
            {
                Id = 1,
                Name = "İzmir",
                CountryId = 1,
                IsActive = true,
                IsDeleted = false
            });
        }
    }
}
