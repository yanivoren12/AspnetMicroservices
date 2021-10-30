using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oredering.Application.Contracts.Infrastructure;
using Oredering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings EmailSettings { get; set; }
        public ILogger<EmailService> Logger { get; set; }

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            EmailSettings = emailSettings.Value;
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendEmail(Email email)
        {
            SendGridClient client = new(EmailSettings.ApiKey);

            string subject = email.Subject;
            EmailAddress to = new(email.To);
            string emailBody = email.Body;

            EmailAddress from = new()
            {
                Email = EmailSettings.FromAddress,
                Name = EmailSettings.FromName
            };

            SendGridMessage sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            Response response = await client.SendEmailAsync(sendGridMessage);

            Logger.LogInformation("Email sent.");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            Logger.LogError("Email sending failed.");
            return false;
        }
    }
}
