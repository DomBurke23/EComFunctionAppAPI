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
            services.AddTransient<IValidator<CreateOrderRequest>, CreateOrderRequestValidator>();
            services.AddTransient<IUseCase<CreateOrderRequest, CreateOrderResponse>, CreateOrderUseCase>();
            
            services.AddTransient<IValidator<GetOrderRequest>, GetOrderRequestValidator>();
            services.AddTransient<IUseCase<GetOrderRequest, GetOrderResponse>, GetOrderUseCase>();
            
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
