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
    public class InterviewBuilder : IEntityTypeConfiguration<Interview>
    {
        public void Configure(EntityTypeBuilder<Interview> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.MeetingDate).IsRequired();

            builder.Property(x => x.InterviewStatus).IsRequired();
            builder.Property(x => x.InterviewStatus).HasConversion<int>();

            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();


            // Relationships


            builder
                .HasOne<Application>(x => x.Application)
                .WithOne()
                .HasForeignKey<Interview>(x => x.ApplicationId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasOne<User>(x => x.InterviewerUser)
                .WithMany(x => x.Interviews)
                .HasForeignKey(x => x.InterviewerUserId)
                .OnDelete(DeleteBehavior.ClientCascade);


            builder.ToTable("Interviews");
        }
    }
}
