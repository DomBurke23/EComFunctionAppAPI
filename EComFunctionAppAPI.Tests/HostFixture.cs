using EComFunctionAppAPI.Data.Repositories;
using EComFunctionAppAPI.Options;
using EComFunctionAppAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EComFunctionAppAPI.Tests;

public class HostFixture
{
    public IHost Host { get; }

    public HostFixture()
    {
        Host = new HostBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.test.json", false);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Repositories 
                services.AddTransient<IRepository, Repository>();

                // Add Options 
                services.AddOptions<DbOptions>().Configure<IConfiguration>((settings, config) =>
                {
                    config.GetSection(nameof(DbOptions)).Bind(settings);
                });

                services.AddSingleton<HttpClient>();
                services.AddTransient<ISaveOrderService, SaveOrderService>();
            }).Build();
    }
}
