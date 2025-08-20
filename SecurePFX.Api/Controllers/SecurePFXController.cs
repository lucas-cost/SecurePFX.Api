using Microsoft.AspNetCore.Mvc;
using SecurePFX.Api.Resources;
using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;
using SecurePFX.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SecurePFX.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecurePFXController : ControllerBase
    {
        private readonly ILogger<SecurePFXController> _logger;
        private readonly ICertificateService _certificateService;

        public SecurePFXController(ILogger<SecurePFXController> logger, ICertificateService certificateService)
        {
            _logger = logger;
            _certificateService = certificateService;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadCertificate([FromForm] CertificateUploadDTO uploadCertificateDTO, CancellationToken cancellationToken)
        {
            try
            {
                CertificateResponseDTO response = await _certificateService.ProcessAndStoreCertificateAsync(uploadCertificateDTO, cancellationToken);
                
                return Ok(new
                {
                    Success = true,
                    Data = response,
                    Message = ApiMsg.API001,
                });
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ApiMsg.APILOG001);
                return StatusCode(500, ApiMsg.API002);
            }
        }
    }
}
