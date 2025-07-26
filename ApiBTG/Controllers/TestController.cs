using ApiBTG.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiBTG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly EmailNotificationService _emailService;
        private readonly SmsNotificationService _smsService;

        public TestController(EmailNotificationService emailService, SmsNotificationService smsService)
        {
            _emailService = emailService;
            _smsService = smsService;
        }

        [HttpPost("send-test-email")]
        public async Task<IActionResult> SendTestEmail([FromQuery] string to)
        {
            try
            {
                await _emailService.SendAsync(
                    to,
                    "Prueba de Email desde ApiBTG",
                    "<h2>¡Funciona el envío de correo!</h2><p>Este es un mensaje de prueba desde tu API BTG.</p>"
                );
                return Ok("Correo enviado exitosamente (verifica tu bandeja de Mailtrap)");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al enviar email: {ex.Message}");
            }
        }

        [HttpPost("send-test-sms")]
        public async Task<IActionResult> SendTestSms([FromQuery] string to)
        {
            try
            {
                await _smsService.SendAsync(
                    to,
                    "",
                    "¡Funciona el envío de SMS desde ApiBTG!"
                );
                return Ok("SMS enviado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al enviar SMS: {ex.Message}");
            }
        }
    }
} 