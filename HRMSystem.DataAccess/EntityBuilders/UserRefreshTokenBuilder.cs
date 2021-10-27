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
    public class UserRefreshTokenBuilder : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Token).IsRequired();
            builder.Property(x => x.Token).HasMaxLength(255);

            builder.Property(x => x.ExpirationDate).IsRequired();


            // Relationships

            builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);


            builder.ToTable("UserRefreshTokens");
        }
    }
}
