namespace EComFunctionAppAPI.Client.Responses;

public class CreateOrderResponse
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string Error { get; set; }
}
