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
    public class ApplicantWorkExperienceBuilder : IEntityTypeConfiguration<ApplicantWorkExperience>
    {
        public void Configure(EntityTypeBuilder<ApplicantWorkExperience> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CompanyName).IsRequired();
            builder.Property(x => x.CompanyName).HasMaxLength(128);

            builder.Property(x => x.StartingDate).IsRequired();

            builder.Property(x => x.EndingDate).IsRequired(false);


            // Relationships

            builder
                .HasOne<WorkTitle>(x => x.WorkTitle)
                .WithMany()
                .HasForeignKey(x => x.WorkTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.ToTable("ApplicantWorkExperiences");
        }
    }
}
