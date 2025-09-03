using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurePFX.Api.Resources;
using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;
using SecurePFX.Application.Interfaces;
using SecurePFX.Application.Messages;
using System.ComponentModel.DataAnnotations;

namespace SecurePFX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificateController : ControllerBase
    {
        private readonly ILogger<CertificateController> _logger;
        private readonly ICertificateService _certificateService;
        private readonly IMapper _mapper;

        public CertificateController(ILogger<CertificateController> logger, ICertificateService certificateService, IMapper mapper)
        {
            _logger = logger;
            _certificateService = certificateService;
            _mapper = mapper;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> UploadCertificate([FromForm] CertificateUploadDTO uploadCertificateDTO, CancellationToken cancellationToken, [FromServices] IBus bus)
        {
            try
            {
                CertificateResponseDTO response = await _certificateService.ProcessAndStoreCertificateAsync(uploadCertificateDTO, cancellationToken);

                CertificateUploadedMessageEvent certificateUploadedMessageEvent = _mapper.Map<CertificateUploadedMessageEvent>(response);

                await bus.Publish(certificateUploadedMessageEvent, cancellationToken);

                _logger.LogInformation("Message published to RabbitMQ: CertificateId={CertificateId}", response.Id);

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

        [HttpGet("get")]
        public async Task<IActionResult> GetCertificateAsync()
        {
            return Ok("SecurePFX Api is running...");   
        }
    }
}
