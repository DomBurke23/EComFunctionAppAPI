using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EComFunctionAppAPI.Middleware;

public interface IAuthorizationMiddleware
{
    Task InvokeAsync(HttpRequest httpRequest);
}
