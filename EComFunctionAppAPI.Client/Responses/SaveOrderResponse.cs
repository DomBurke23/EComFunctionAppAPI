namespace EComFunctionAppAPI.Client.Responses;

public class SaveOrderResponse
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string Error { get; set; }
}
