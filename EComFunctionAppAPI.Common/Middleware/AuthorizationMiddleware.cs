using EComFunctionAppAPI.Client.Constants;
using EComFunctionAppAPI.Common.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EComFunctionAppAPI.Common.Middleware
{
    public class AuthorizationMiddleware : IAuthorizationMiddleware
    {
        private readonly IOptions<AuthorizationOptions> _options;

        public AuthorizationMiddleware(IOptions<AuthorizationOptions> options)
        {
            _options = options;
        }

        public async Task InvokeAsync(HttpRequest httpRequest)
        {
            var expectedApiKey = _options.Value.ApiKey;

            if (string.IsNullOrWhiteSpace(expectedApiKey) || expectedApiKey == "disabled") return;

            if (!httpRequest.Headers.TryGetValue(HttpRequestHeaderConstants.XApiKey, out var xApiKey))
                throw new UnauthorizedAccessException("Api Key not present");

            if (xApiKey != expectedApiKey) throw new UnauthorizedAccessException("Invalid Api Key");
        }
    }
}
