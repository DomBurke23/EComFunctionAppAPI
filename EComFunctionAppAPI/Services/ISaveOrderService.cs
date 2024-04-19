using EComFunctionAppAPI.Client.Requests;
using EComFunctionAppAPI.Responses;
using System.Threading.Tasks;

namespace EComFunctionAppAPI.Services;

public interface ISaveOrderService
{
    Task<SaveOrderResponse> HandleAsync(SaveOrderRequest saveOrderRequest);
}
