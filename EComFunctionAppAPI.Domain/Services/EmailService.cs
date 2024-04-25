using Microsoft.Extensions.Logging;

namespace EComFunctionAppAPI.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string email)
        {
            _logger.LogInformation($"Sending email to {email}");
            var response = true;
            return response;
        }
    }
}