using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RPA_Test_New.Application.Selenium;
using RPA_Test_New.Application.Selenium.Controllers;
using RPA_Test_New.Application.Selenium.Pages.Alura;
using RPA_Test_New.Domain.Interfaces;
using RPA_Test_New.Infrastructure.Data.Repositories;
using RPA_Test_New.Infrastructure.Services;

namespace RPA_Test_New.Application.Configuration
{
    public static class AppDependencyInjection
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //Repositório
            services.AddTransient<IRpaRepository, RpaRepository>();

            //Serviços
            services.AddSingleton<INavigator, Navigator>();

            //Controllers
            services.AddTransient<AluraController>();

            //Páginas
            services.AddTransient<HomePage>();
            

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
