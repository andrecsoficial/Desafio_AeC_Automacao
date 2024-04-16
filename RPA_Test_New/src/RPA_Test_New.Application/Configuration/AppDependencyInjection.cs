using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RPA_Test_New.Domain.Interfaces;
using RPA_Test_New.Infrastructure.Services;

namespace RPA_Test_New.Application.Configuration
{
    public static class AppDependencyInjection
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //Repositório


            //Serviços


            //Controllers


            //Páginas

            services.AddSingleton<IDriverFactoryService>(_ =>
            {
                var driverFactory = new DriverFactoryService();
                AppDomain.CurrentDomain.ProcessExit += (_, _) => driverFactory?.Instance?.Quit();
                AppDomain.CurrentDomain.UnhandledException += (_, _) => driverFactory?.Instance?.Quit();
                return driverFactory;
            });
            return services;

        }
    }
}
