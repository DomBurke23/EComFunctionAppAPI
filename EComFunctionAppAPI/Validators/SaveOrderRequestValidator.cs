using EComFunctionAppAPI.Requests;
using FluentValidation;

namespace EComFunctionAppAPI.Validators;

public class SaveOrderRequestValidator : AbstractValidator<SaveOrderRequest>
{
    public SaveOrderRequestValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.TotalPrice).NotEmpty();
    }
}
