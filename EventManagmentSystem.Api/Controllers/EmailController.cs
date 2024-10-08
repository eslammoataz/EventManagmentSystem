using EventManagmentSystem.Application.Services.EmailService;
using Microsoft.AspNetCore.Mvc;

namespace EventManagmentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
        {
            await _emailService.SendEmailAsync(emailRequest.RecipientEmail, emailRequest.Subject, emailRequest.Body);
            return Ok(new { message = "Email sent successfully." });
        }
    }

    public class EmailRequest
    {
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
