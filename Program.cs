using DatabaseSynchServiceUsingWorkerService;
using DatabaseSynchServiceUsingWorkerService.Models;
using Microsoft.EntityFrameworkCore;



IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, configBuilder) =>
    {
        // Add the configuration from appsettings.json to the configuration builder
        configBuilder.AddJsonFile("appsettings.json"); // Update with the correct path if needed
    })
    .ConfigureServices((hostContext,services) =>
    {
        services.AddDbContext<AppDbContext>(o => o.UseSqlServer(hostContext.Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value));

        services.AddHostedService<Worker>();
        
    })
    .Build();
var configSetting = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json").Build();

host.Run();
