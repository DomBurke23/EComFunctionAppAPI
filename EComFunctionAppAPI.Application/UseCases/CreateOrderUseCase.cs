using EComFunctionAppAPI.Application.Interfaces;
using EComFunctionAppAPI.Client.Requests;
using EComFunctionAppAPI.Client.Responses;
using EComFunctionAppAPI.Domain.Services;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EComFunctionAppAPI.Application.UseCases
{
    public class CreateOrderUseCase : IUseCase<CreateOrderRequest, CreateOrderResponse>
    {
        private readonly ILogger<IUseCase<CreateOrderRequest, CreateOrderResponse>> _logger;
        private readonly IOrderRepository _repository;
        private readonly IEmailService _emailService;

        public CreateOrderUseCase(ILogger<IUseCase<CreateOrderRequest, CreateOrderResponse>> logger,
            IOrderRepository repository,
            IEmailService emailService)
        {
            _logger = logger;
            _repository = repository;
            _emailService = emailService;
        }

        public async Task<CreateOrderResponse> HandleAsync(CreateOrderRequest request)
        {
            _logger.LogInformation("Create Order Use Case Entered");

            #region Create / Save Order to DB
            try
            {
                var update = await _repository.CreateOrder(request);

                if (!update)
                {
                    return new CreateOrderResponse
                    {
                        Success = false,
                        StatusCode = (int)HttpStatusCode.ServiceUnavailable,
                        Error = "Error Completing Order."
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception thrown Saving data for Order ID {request.OrderId}");
            }
            #endregion

            #region Email out order
            try
            {
                var emailResponse = await _emailService.SendEmailAsync("email");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception thrown sending email for Order ID {request.OrderId}");
            }
            #endregion

            return new CreateOrderResponse
            {
                Success = true,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
