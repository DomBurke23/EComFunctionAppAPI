using EComFunctionAppAPI.Application.Interfaces;
using EComFunctionAppAPI.Client.Requests;
using EComFunctionAppAPI.Client.Responses;
using EComFunctionAppAPI.Domain.Services;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EComFunctionAppAPI.Application.UseCases
{
    public class SaveOrderUseCase : IUseCase<SaveOrderRequest, SaveOrderResponse>
    {
        private readonly ILogger<IUseCase<SaveOrderRequest, SaveOrderResponse>> _logger;
        private readonly IOrderRepository _repository;
        private readonly IEmailService _emailService;

        public SaveOrderUseCase(ILogger<IUseCase<SaveOrderRequest, SaveOrderResponse>> logger,
            IOrderRepository repository,
            IEmailService emailService)
        {
            _logger = logger;
            _repository = repository;
            _emailService = emailService;
        }

        public async Task<SaveOrderResponse> HandleAsync(SaveOrderRequest request)
        {
            _logger.LogInformation("Save Order Use Case Entered");

            #region Save Order to DB
            try
            {
                var update = await _repository.SaveOrder(request);

                if (!update)
                {
                    return new SaveOrderResponse
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

            return new SaveOrderResponse
            {
                Success = true,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
