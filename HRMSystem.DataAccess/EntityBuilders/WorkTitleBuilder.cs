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
    public class WorkTitleBuilder : IEntityTypeConfiguration<WorkTitle>
    {
        public void Configure(EntityTypeBuilder<WorkTitle> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50);


            builder.ToTable("WorkTitles");

            builder.HasData(new WorkTitle
            {
                Id = 1,
                Name = "Backend Developer",
                IsActive = true,
                IsDeleted = false
            });
        }
    }
}
