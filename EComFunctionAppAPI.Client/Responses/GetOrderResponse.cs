namespace EComFunctionAppAPI.Client.Responses
{
    public class GetOrderResponse
    {
        public Order Order { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string OrderID { get; set; }
        public string CustomerID { get; set; }
    }
}
