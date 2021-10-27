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
    public class ApplicantEducationBuilder : IEntityTypeConfiguration<ApplicantEducation>
    {
        public void Configure(EntityTypeBuilder<ApplicantEducation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.SchoolName).IsRequired();
            builder.Property(x => x.SchoolName).HasMaxLength(128);

            builder.Property(x => x.DepartmentName).IsRequired();
            builder.Property(x => x.DepartmentName).HasMaxLength(128);

            builder.Property(x => x.EducationDegree).IsRequired();
            builder.Property(x => x.EducationDegree).HasConversion<int>();

            builder.Property(x => x.StartingDate).IsRequired();

            builder.Property(x => x.EndingDate).IsRequired(false);

            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();


            // Relationships

            builder
                .HasOne<Application>()
                .WithMany(x => x.ApplicantEducations)
                .HasForeignKey(x => x.ApplicationId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.ToTable("ApplicantEducations");
        }
    }
}
