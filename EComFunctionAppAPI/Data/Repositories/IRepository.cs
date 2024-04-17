using EComFunctionAppAPI.Requests;
using System.Threading.Tasks;

namespace EComFunctionAppAPI.Data.Repositories;

public interface IRepository
{
    Task<bool> SaveOrder(SaveOrderRequest saveOrderRequest);
}
