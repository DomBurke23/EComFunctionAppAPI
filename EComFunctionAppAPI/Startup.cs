using EComFunctionAppAPI;
using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EComFunctionAppAPI.Extensions;
using EComFunctionAppAPI.Common.Extensions;
using EComFunctionAppAPI.Common.Options;
using EComFunctionAppAPI.Infrastructure.Options;
using EComFunctionAppAPI.Infrastructure.Extensions;

[assembly: FunctionsStartup(typeof(Startup))]
namespace EComFunctionAppAPI;

public class Startup : FunctionsStartup
{

    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddAuthorizationServices();
        builder.Services.AddOptions<AuthorizationOptions>().Configure<IConfiguration>((settings, config) =>
        {
            config.GetSection(nameof(AuthorizationOptions)).Bind(settings);
        });

        ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

        builder.Services.AddApplicationServices();

        builder.Services.AddDataServices();
        builder.Services.AddOptions<DbOptions>().Configure<IConfiguration>((settings, config) =>
        {
            config.GetSection(nameof(DbOptions)).Bind(settings);
        });
    }
}
