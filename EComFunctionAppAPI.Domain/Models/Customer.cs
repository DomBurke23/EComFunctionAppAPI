namespace EComFunctionAppAPI.Domain.Models;

public class Customer
{
    public string CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Postcode { get; set; }
    public string EmailAddress { get; set; }
    // If the customer chooses the checkout as guest option they will not be registered 
    // Data Job to remove unregistered users after X days from Database OR have an alternatives tables. 
    public bool Registered { get; set; }
}
