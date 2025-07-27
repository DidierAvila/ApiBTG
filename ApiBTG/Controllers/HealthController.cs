using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBTG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "administrador")]
        public IActionResult Health()
        {
            _logger.LogInformation("Health check requested at {Time}", DateTime.UtcNow);
            
            return Ok(new
            {
                status = "healthy",
                timestamp = DateTime.UtcNow,
                version = "1.0.0",
                environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown"
            });
        }

        [HttpGet("ready")]
        [Authorize(Roles = "administrador")]
        public IActionResult Ready()
        {
            // Aquí puedes agregar verificaciones adicionales como:
            // - Conexión a base de datos
            // - Servicios externos
            // - Recursos del sistema
            
            return Ok(new
            {
                status = "ready",
                timestamp = DateTime.UtcNow,
                checks = new
                {
                    database = "healthy",
                    external_services = "healthy"
                }
            });
        }
    }
} 