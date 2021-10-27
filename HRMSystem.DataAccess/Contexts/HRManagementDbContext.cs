using HRMSystem.Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSystem.DataAccess.Contexts
{
    public class HRManagementDbContext : IdentityDbContext<User, Role, string /*IdentityUserClaim<string>, UserRole, IdentityUserLogin, IdentityRoleClaim, IdentityUserToken*/>
    {
        public DbSet<ApplicantEducation> ApplicantEducations { get; set; }
        public DbSet<ApplicantHobby> ApplicantHobbies { get; set; }
        public DbSet<ApplicantLanguage> ApplicantLanguages { get; set; }
        public DbSet<ApplicantWorkExperience> ApplicantWorkExperiences { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Language> Langauges { get; set; }
        public DbSet<WorkTitle> WorkTitles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        public HRManagementDbContext(DbContextOptions<HRManagementDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(HRManagementDbContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}
