using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EventManagmentSystem.Application.Services.EmailService
{
    public class GmailEmailService : IEmailService
    {
        private readonly GmailSettings _gmailSettings;
        private readonly ILogger _logger;

        public GmailEmailService(IOptions<GmailSettings> gmailSettings, ILogger<GmailEmailService> logger)
        {
            _gmailSettings = gmailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            try
            {
                _logger.LogInformation("Gmail settings - SmtpUsername: {SmtpUsername}, SmtpPassword: {SmtpPassword}," +
                    " FromEmail: {FromEmail}", _gmailSettings.SmtpUsername, _gmailSettings.SmtpPassword, _gmailSettings.FromEmail);

                var smtpClient = new SmtpClient(_gmailSettings.SmtpServer, _gmailSettings.SmtpPort)
                {
                    Credentials = new NetworkCredential(_gmailSettings.SmtpUsername, _gmailSettings.SmtpPassword),
                    EnableSsl = true // Ensures SSL encryption is used
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_gmailSettings.FromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true // Set to false if you are sending plain text email
                };

                mailMessage.To.Add(recipientEmail);

                // Send the email asynchronously
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle exceptions or log the error as needed
                throw new InvalidOperationException($"Error sending email: {ex.Message}", ex);
            }
        }
    }
}