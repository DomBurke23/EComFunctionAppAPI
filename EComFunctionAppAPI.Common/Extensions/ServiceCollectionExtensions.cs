using EComFunctionAppAPI.Common.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace EComFunctionAppAPI.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthorizationServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthorizationMiddleware, AuthorizationMiddleware>();
        }
    }
}
