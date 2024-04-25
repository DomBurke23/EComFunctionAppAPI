using EComFunctionAppAPI.Application.UseCases;
using EComFunctionAppAPI.Client.Requests;
using EComFunctionAppAPI.Client.Responses;
using EComFunctionAppAPI.Domain.Services;
using EComFunctionAppAPI.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EComFunctionAppAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IValidator<SaveOrderRequest>, SaveOrderRequestValidator>();
            services.AddTransient<IUseCase<SaveOrderRequest, SaveOrderResponse>, SaveOrderUseCase>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
