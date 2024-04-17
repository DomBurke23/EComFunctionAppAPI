﻿using EComFunctionAppAPI;
using EComFunctionAppAPI.Middleware;
using EComFunctionAppAPI.Options;
using EComFunctionAppAPI.Requests;
using EComFunctionAppAPI.Validators;
using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace EComFunctionAppAPI;

public class Startup : FunctionsStartup
{

    public override void Configure(IFunctionsHostBuilder builder)
    {
        //builder.Services.AddHttpClient();

        // Middleware
        builder.Services.AddTransient<IAuthorizationMiddleware, AuthorizationMiddleware>();
        builder.Services.AddOptions<AuthorizationOptions>().Configure<IConfiguration>((settings, config) =>
        {
            config.GetSection(nameof(AuthorizationOptions)).Bind(settings);
        });

        // Validation 
        ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;
        builder.Services.AddTransient<IValidator<SaveOrderRequest>, SaveOrderRequestValidator>();
    }
}