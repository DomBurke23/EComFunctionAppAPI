using Microsoft.AspNetCore.Http;

namespace EComFunctionAppAPI.Common.Middleware;

public interface IAuthorizationMiddleware
{
    Task InvokeAsync(HttpRequest httpRequest);
}
