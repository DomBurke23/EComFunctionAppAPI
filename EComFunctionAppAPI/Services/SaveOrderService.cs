using EComFunctionAppAPI.Client.Requests;
using EComFunctionAppAPI.Data.Repositories;
using EComFunctionAppAPI.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EComFunctionAppAPI.Services
{
    public class SaveOrderService : ISaveOrderService
    {
        private readonly ILogger<SaveOrderService> _logger;
        private readonly IRepository _repository;

        public SaveOrderService(ILogger<SaveOrderService> logger,
            IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<SaveOrderResponse> HandleAsync(SaveOrderRequest saveOrderRequest)
        {
            _logger.LogInformation("Save Order Service Entered.");

            #region Save Order to DB
            try
            {
                var update = await _repository.SaveOrder(saveOrderRequest);

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
                _logger.LogError(ex, $"Error Occurred Saving data for Order ID {saveOrderRequest.OrderId}");
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