using EComFunctionAppAPI.Application.Interfaces;
using EComFunctionAppAPI.Client.Requests;
using EComFunctionAppAPI.Client.Responses;
using EComFunctionAppAPI.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace EComFunctionAppAPI.Application.UseCases
{
    public class GetOrderUseCase : IUseCase<GetOrderRequest, GetOrderResponse>
    {
        private readonly ILogger<IUseCase<GetOrderRequest, GetOrderResponse>> _logger;
        private readonly IOrderRepository _repository;

        public GetOrderUseCase(ILogger<IUseCase<GetOrderRequest, GetOrderResponse>> logger,
            IOrderRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<GetOrderResponse> HandleAsync(GetOrderRequest request)
        {
            _logger.LogInformation("Get Order Use Case Entered");

            var order = await _repository.GetOrderByOrderId(request.OrderId);

            return order is null
                ? throw new OrderNotFoundException($"Order {request.OrderId} not found.")
                : new GetOrderResponse {  };
        }
    }
}
