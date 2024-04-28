using EComFunctionAppAPI.Client.Requests;
using FluentValidation;

namespace EComFunctionAppAPI.Validators;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.TotalPrice).NotEmpty();
    }
}
