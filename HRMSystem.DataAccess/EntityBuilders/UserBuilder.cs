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
    public class UserBuilder : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName).IsRequired(false);
            builder.Property(x => x.FirstName).HasMaxLength(50);

            builder.Property(x => x.LastName).IsRequired(false);
            builder.Property(x => x.LastName).HasMaxLength(50);

            builder.Property(x => x.WorkingStatus).IsRequired(true);
            builder.Property(x => x.WorkingStatus).HasConversion<int>();

            builder.Property(u => u.PhoneNumber).IsRequired(false);
            builder.Property(u => u.PhoneNumber).HasMaxLength(20);

            builder.HasIndex(x => x.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            builder.HasIndex(x => x.NormalizedEmail).HasDatabaseName("EmailIndex");

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();


            // Relationships

            builder.HasOne<City>(x => x.City).WithMany().IsRequired(false);
            builder.HasOne<Country>(x => x.Country).WithMany().IsRequired(false);

            builder.ToTable("AspNetUsers");
        }
    }
}
