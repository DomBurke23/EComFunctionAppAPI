using EComFunctionAppAPI.Client.Requests;
using EComFunctionAppAPI.Domain.Models;

namespace EComFunctionAppAPI.Application.Interfaces;

public interface IOrderRepository
{
    Task<bool> CreateOrder(CreateOrderRequest createOrderRequest);
    Task<Order> GetOrderByOrderId(string orderId);
}
