using RPA_Test_New.Application;
using RPA_Test_New.Application.Configuration;

IConfiguration Configuration = default;
IHostEnvironment HostingEnvironment = default;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        HostingEnvironment = context.HostingEnvironment;
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .AddJsonFile("appsettings.json")
            .AddJsonFile("AppSettingsNavigation.json")
            .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json")
            .AddEnvironmentVariables()
            .Build();

        services.AddSingleton<IConfiguration>(Configuration);
        services.AddAppDependencies(Configuration);
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            q.AddJobAndTrigger<AluraJob>(Configuration);

        })
        .AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        services.AddLogging(logger =>
        {
            logger.ClearProviders();
            logger.SetMinimumLevel(LogLevel.Information);
            logger.AddNLog("NLog.config");
        });
    })
    .UseWindowsService()
    .Build();

await host.RunAsync();