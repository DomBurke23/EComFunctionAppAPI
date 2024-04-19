using Dapper;
using EComFunctionAppAPI.Data.Models;
using EComFunctionAppAPI.Options;
using EComFunctionAppAPI.Client.Requests;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EComFunctionAppAPI.Data.Repositories;

public class Repository : BaseRepository, IRepository
{
    public Repository(IOptions<DbOptions> dbOptions) : base(dbOptions)
    {

    }

    public async Task<bool> SaveOrder(SaveOrderRequest saveOrderRequest)
    {
        var sql = $@"
INSERT INTO [dbo].[Orders]
(
{nameof(saveOrderRequest.OrderId)},
{nameof(saveOrderRequest.TotalPrice)}
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
                saveOrderRequest.OrderId,
                saveOrderRequest.TotalPrice
            });
            return true;
        }
    }

    public async Task<List<Orders>> GetCustomersOrders(string customerId)
    {
        var sql = $@"
SELECT * 
FROM [dbo].[Customer] c 
INNER JOIN [dbo].[Orders] o 
ON o.CustomerId = c.CustomerId 
WHERE CustomerId = @CustomerId;
";
        using (var connection = new SqlConnection(_dbOptions.ConnectionString))
        {
            connection.Open();
            var orders = await connection.QueryFirstOrDefaultAsync<List<Orders>>(sql, new { CustomerId = customerId });
            return orders;
        }
    }
}
