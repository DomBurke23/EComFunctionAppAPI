namespace EComFunctionAppAPI.Requests;

public class SaveOrderRequest
{
    public string OrderId { get; set; }
    public string CustomerId { get; set; }
    public int TotalPrice { get; set; }
}
