using HRMSystem.Core.Entities.Concrete;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using HRMSystem.DataAccess.Contexts;
using FluentValidation.AspNetCore;
using System.Reflection;
using HRMSystem.Service.Concretes;
using HRMSystem.Service.Abstracts;
using HRMSystem.Core.Security.Helpers;
using FluentValidation;

namespace HRMSystem.Service.Extensions
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<User, Role>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireNonAlphanumeric = true;
                })
                .AddEntityFrameworkStores<HRManagementDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHelper, JwtHelper>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IInterviewService, InterviewService>();

            services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
