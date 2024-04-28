using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EComFunctionAppAPI.Application.UseCases;
using EComFunctionAppAPI.Client.Constants;
using EComFunctionAppAPI.Client.Requests;
using EComFunctionAppAPI.Client.Responses;
using EComFunctionAppAPI.Common.Middleware;
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
    public class OrderTrigger
    {
        private readonly IAuthorizationMiddleware _authorizationMiddleware;
        private readonly IValidator<CreateOrderRequest> _createOrderRequestValidator;
        private readonly IUseCase<CreateOrderRequest, CreateOrderResponse> _createOrderUseCase;
        private readonly IValidator<GetOrderRequest> _getOrderRequestValidator;
        private readonly IUseCase<GetOrderRequest, GetOrderResponse> _getOrderUseCase;

        public OrderTrigger(IAuthorizationMiddleware authorizationMiddleware,
            IValidator<CreateOrderRequest> createOrderRequestValidator,
            IUseCase<CreateOrderRequest, CreateOrderResponse> createOrderUseCase,
            IValidator<GetOrderRequest> getOrderRequestValidator,
            IUseCase<GetOrderRequest, GetOrderResponse> getOrderUseCase)
        {
            _authorizationMiddleware = authorizationMiddleware;
            _createOrderRequestValidator = createOrderRequestValidator;
            _createOrderUseCase = createOrderUseCase;
            _getOrderRequestValidator = getOrderRequestValidator;
            _getOrderUseCase = getOrderUseCase;
        }

        [FunctionName("CreateOrderTrigger")]
        [OpenApiOperation("post-createOrder", "Create / Save an Order.")]
        [OpenApiSecurity(HttpRequestHeaderConstants.XApiKey, SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody("application/json", typeof(CreateOrderRequest), Description = "Create an Order", Required = true)]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string), Description = "Create / Save an Order.")]
        public async Task<IActionResult> CreateOrder(
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

            log.LogInformation("Create new order trigger entered.");

            #region Parse Body
            var input = await req.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<CreateOrderRequest>(input);
            #endregion

            #region Validate request
            var validationResult = _createOrderRequestValidator.Validate(request);
            if (!validationResult.IsValid)
                return new BadRequestObjectResult(validationResult.Errors.Select(e => new
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            #endregion

            #region Create Order Logic 
            var response = await _createOrderUseCase.HandleAsync(request);
            #endregion

            return new OkObjectResult(response);
        }


        [FunctionName("GetOrderTrigger")]
        [OpenApiOperation("get-order", "Retrieve an Order.")]
        [OpenApiSecurity(HttpRequestHeaderConstants.XApiKey, SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "orderId", Description = "Order ID", Type = typeof(string), Required = true, In = ParameterLocation.Query)]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string), Description = "Retrieve an Order.")]
        public async Task<IActionResult> GetOrder(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
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

            log.LogInformation("Get order trigger entered.");

            #region Parse Input
            var orderId = req.Query["orderId"];
            var request = new GetOrderRequest { OrderId = orderId };
            #endregion

            #region Validate request
            var validationResult = _getOrderRequestValidator.Validate(request);
            if (!validationResult.IsValid)
                return new BadRequestObjectResult(validationResult.Errors.Select(e => new
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                }));
            #endregion

            #region Get Order Logic 
            var response = await _getOrderUseCase.HandleAsync(request);
            #endregion

            return new OkObjectResult(response);
        }
    }
}

