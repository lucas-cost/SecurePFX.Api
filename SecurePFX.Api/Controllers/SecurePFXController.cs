using Microsoft.AspNetCore.Mvc;

namespace SecurePFX.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecurePFXController : ControllerBase
    {
        private readonly ILogger<SecurePFXController> _logger;

        public SecurePFXController(ILogger<SecurePFXController> logger)
        {
            _logger = logger;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadCertificate([FromRoute] IFormFile file, CancellationToken cancellationToken)
        {
            return Ok("Certificado enviado com sucesso.");
        }
    }
}
