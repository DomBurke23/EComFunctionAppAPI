using EComFunctionAppAPI.Requests;
using EComFunctionAppAPI.Responses;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EComFunctionAppAPI.Services
{
    public class SaveOrderService : ISaveOrderService
    {
        private readonly ILogger<SaveOrderService> _logger;

        public SaveOrderService(ILogger<SaveOrderService> logger)
        {
            _logger = logger;
        }

        public Task<SaveOrderResponse> HandleAsync(SaveOrderRequest saveOrderRequest)
        {
            _logger.LogInformation("Save Order Service Entered.");
            // TODO Logic here 
            throw new System.NotImplementedException();
        }
    }
}
