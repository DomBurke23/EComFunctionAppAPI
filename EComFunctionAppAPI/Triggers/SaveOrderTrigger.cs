using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EComFunctionAppAPI.Constants;
using EComFunctionAppAPI.Middleware;
using EComFunctionAppAPI.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace EComFunctionAppAPI.Triggers
{
    public class SaveOrderTrigger
    {
        private readonly IAuthorizationMiddleware _authorizationMiddleware;
        private readonly IValidator<SaveOrderRequest> _saveOrderRequestValidator;

        public SaveOrderTrigger(IAuthorizationMiddleware authorizationMiddleware,
            IValidator<SaveOrderRequest> saveOrderRequestValidator)
        {
            _authorizationMiddleware = authorizationMiddleware;
            _saveOrderRequestValidator = saveOrderRequestValidator;
        }

        [FunctionName("SaveOrderTrigger")]
        [OpenApiOperation("post-saveorder", "Save an Order.")]
        [OpenApiSecurity(HttpRequestHeaderConstants.XApiKey, SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody("application/json", typeof(SaveOrderRequest), Description = "Change a users email", Required = true)]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string), Description = "Save an Order.")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            #region Validate User
            try
            {
                await _authorizationMiddleware.InvokeAsync(req);
            }
            catch (UnauthorizedAccessException)
            {
                return new UnauthorizedResult();
            }
            #endregion

            log.LogInformation("Save new order trigger entered.");

            #region Parse Body
            var input = await req.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<SaveOrderRequest>(input);
            #endregion

            #region Validate request
            var validationResult = _saveOrderRequestValidator.Validate(request);
            if (!validationResult.IsValid)
                return new BadRequestObjectResult(validationResult.Errors.Select(e => new
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            #endregion

            #region Logic 

            #endregion

            return new OkObjectResult("");
        }
    }
}

