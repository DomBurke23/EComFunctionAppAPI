using EComFunctionAppAPI.Client.Requests;
using FluentValidation;

namespace EComFunctionAppAPI.Validators;

public class GetOrderRequestValidator : AbstractValidator<GetOrderRequest>
{
    public GetOrderRequestValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
    }
}
