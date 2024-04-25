using EComFunctionAppAPI.Application.Interfaces;
using EComFunctionAppAPI.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EComFunctionAppAPI.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddSingleton<IOrderRepository, OrderRepository>();
        }
    }
}
