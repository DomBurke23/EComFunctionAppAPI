using EComFunctionAppAPI.Client.Requests;

namespace EComFunctionAppAPI.Client;

public interface IEComClient
{
    // The API Triggers we can call will be added here 
    Task<HttpResponseMessage> SaveOrderAsync(SaveOrderRequest request);
}
