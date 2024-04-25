using EComFunctionAppAPI.Client.Requests;

namespace EComFunctionAppAPI.Application.Interfaces;

public interface IOrderRepository
{
    Task<bool> SaveOrder(SaveOrderRequest saveOrderRequest);
}
