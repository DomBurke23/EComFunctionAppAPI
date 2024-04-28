namespace EComFunctionAppAPI.Client.Requests;

public class CreateOrderRequest
{
    public string OrderId { get; set; }
    public string CustomerId { get; set; }
    public int TotalPrice { get; set; }
}
