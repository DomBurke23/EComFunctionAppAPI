using Dapper;
using EComFunctionAppAPI.Client.Requests;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using EComFunctionAppAPI.Infrastructure.Options;
using EComFunctionAppAPI.Domain.Models;
using EComFunctionAppAPI.Application.Interfaces;

namespace EComFunctionAppAPI.Data.Repositories;

public class OrderRepository : BaseRepository, IOrderRepository
{
    public OrderRepository(IOptions<DbOptions> dbOptions) : base(dbOptions)
    {

    }

    public async Task<bool> CreateOrder(CreateOrderRequest createOrderRequest)
    {
        var sql = $@"
INSERT INTO [dbo].[Order]
(
{nameof(createOrderRequest.OrderId)},
{nameof(createOrderRequest.TotalPrice)}
)
VALUES
(
@OrderId,
@TotalPrice
);
";

        using (var connection = new SqlConnection(_dbOptions.ConnectionString))
        {
            connection.Open();
            await connection.ExecuteAsync(sql, new
            {
                createOrderRequest.OrderId,
                createOrderRequest.TotalPrice
            });
            return true;
        }
    }

    public async Task<Order> GetOrderByOrderId(string orderId)
    {
        var sql = $@"
SELECT * 
FROM [dbo].[Order] o
Where o.OrderId = @OrderId;
";
        using (var connection = new SqlConnection(_dbOptions.ConnectionString))
        {
            connection.Open();
            var order = await connection.QueryFirstOrDefaultAsync<Order>(sql, new { OrderId = orderId });
            return order;
        }
    }

    public async Task<List<Order>> GetCustomersOrders(string customerId)
    {
        var sql = $@"
SELECT * 
FROM [dbo].[Customer] c 
INNER JOIN [dbo].[Order] o 
ON o.CustomerId = c.CustomerId 
WHERE CustomerId = @CustomerId;
";
        using (var connection = new SqlConnection(_dbOptions.ConnectionString))
        {
            connection.Open();
            var orders = await connection.QueryFirstOrDefaultAsync<List<Order>>(sql, new { CustomerId = customerId });
            return orders;
        }
    }
}
