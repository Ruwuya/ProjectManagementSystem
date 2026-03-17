using System.Configuration;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Infrastructure.Data;
using System.Windows;
using ProjectManagement.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Application.Services;
using ProjectManagement.Infrastructure.Repositories;

namespace ProjectManagement.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        // Static property to hold the service provider for dependency injection
        public static IServiceProvider Services { get; private set; } = null!;

        // Override the OnStartup method to configure services and show the main window
        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // Build the configuration to read from appsettings.json and appsettings.Development.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            ConfigureServices(services, configuration);

            Services = services.BuildServiceProvider();

            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        // Method to configure services for dependency injection
        private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Add the configuration to the service collection so it can be injected into other services if needed
            // Only one instance of this is needed in the application, so we use Singleton lifetime
            services.AddSingleton(configuration);

            // Get the connection string from the configuration (appsettings.development.json or appsettings.json)
            var connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DefaultConnection is missing");

            // Add infrastructure services, including the DbContext, using the connection string
            // This will allow us to inject the DbContext into our services and repositories
            services.AddInfraStructure(connectionString);

            // Register the ProjectService as the implementation of IProjectService with a scoped lifetime
            // Scoped lifetime is used here because we want a new instance of ProjectService for each scope
            // (e.g., each web request or each time it's requested in the UI)
            // The IProjectService will be provided by the dependency injection container when it is requested,
            // and it will receive an instance of IProjectRepository as a dependency
            services.AddScoped<IProjectService, ProjectService>();

            // Register the MainWindow as a transient service so it can be created with its dependencies injected
            // Transient lifetime is used here because we want a new instance of MainWindow each time it is requested,
            // which is appropriate for UI components
            services.AddTransient<MainWindow>();
        }
    }
}
