using EmailService.Domain;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace EmailServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly Service.EmailService _emailService;

        public EmailController(ILogger<EmailController> logger, Service.EmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpPost(Name = "PostEmail")]
        public async Task<IActionResult> Post(Email email)
        {
            _emailService.Save(email);
            return Ok(email);
        }
    }
}
