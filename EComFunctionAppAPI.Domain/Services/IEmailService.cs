namespace EComFunctionAppAPI.Domain.Services;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string email);
}
