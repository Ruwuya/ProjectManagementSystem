using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Infrastructure.Repositories;

namespace ProjectManagement.Infrastructure.DependencyInjection
{
    // Static class because it only contains extension methods and should not be instantiated
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfraStructure(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<ProjectManagementDbContext>(options =>
            options.UseSqlServer(connectionString));

            // Register the ProjectRepository as the implementation of IProjectRepository with a scoped lifetime
            services.AddScoped<IProjectRepository, ProjectRepository>();

            return services;
        }
    }
}
