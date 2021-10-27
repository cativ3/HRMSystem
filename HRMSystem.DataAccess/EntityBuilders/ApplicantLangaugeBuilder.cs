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
    public class ApplicantLangaugeBuilder : IEntityTypeConfiguration<ApplicantLanguage>
    {
        public void Configure(EntityTypeBuilder<ApplicantLanguage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.LanguageLevel).IsRequired();
            builder.Property(x => x.LanguageLevel).HasConversion<int>();


            // Relationships

            builder
                .HasOne<Language>(x => x.Language)
                .WithMany()
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasOne<Application>()
                .WithMany(x => x.ApplicantLanguages)
                .HasForeignKey(x => x.ApplicationId)
                .OnDelete(DeleteBehavior.ClientCascade);


            builder.ToTable("ApplicantLanguages");
        }
    }
}
