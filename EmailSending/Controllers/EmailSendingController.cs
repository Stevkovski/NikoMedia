using Microsoft.AspNetCore.Mvc;
using Services.EmailServices;

namespace EmailSending.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailSendingController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailSendingController> _logger;

        public EmailSendingController(ILogger<EmailSendingController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpGet(Name = "SendEmail")]
        public async Task<IActionResult> Get()
        {
            _emailService.SendEmail("niki_orce@yahoo.com", "Test Subject", "This is a test email.");

            bool result = await _emailService.SendEmailAsync("SendGridApiKey", new Services.Models.EmailRecipients(), "TestSubject", "HTMLContent", false);

            return Ok(result);
        }
    }
}