using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.Interfaces;
using TaskFlow.Infrastructure.Data;
using TaskFlow.Infrastructure.Decorators;
using TaskFlow.Infrastructure.Security;
using TaskFlow.Infrastructure.Services;

namespace TaskFlow.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default")));

            //services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<TaskService>();
            services.AddScoped<ITaskService>(sp =>
            {
                var inner = sp.GetRequiredService<TaskService>();
                var logger = sp.GetRequiredService<ILogger<LoggingTaskService>>();
                return new LoggingTaskService(inner, logger);
            });
            services.AddScoped<IProjectService, ProjectService>();
            //services.AddScoped<IImageStorage, GoogleCloudImageStorage>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<JwtProvider>();
            services.AddScoped<PasswordHasher>();
            services.AddScoped<IProjectAccessService, ProjectAccessService>();
            return services;
        }

    }
}
