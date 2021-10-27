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
    public class ApplicantHobbyBuilder : IEntityTypeConfiguration<ApplicantHobby>
    {
        public void Configure(EntityTypeBuilder<ApplicantHobby> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.Name).HasMaxLength(50);

            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();


            // Relationships

            builder
                .HasOne<Application>()
                .WithMany(x => x.ApplicantHobbies)
                .HasForeignKey(x => x.ApplicationId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.ToTable("ApplicantHobbies");
        }
    }
}
