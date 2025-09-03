using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;
using SecurePFX.Application.Interfaces;

namespace SecurePFX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeCompanyController : ControllerBase
    {
        private readonly IAuthorizeCompanyService _authorizeCompanyService;
        private readonly ILogger<CertificateController> _logger;

        public AuthorizeCompanyController(IAuthorizeCompanyService authorizeCompanyService, ILogger<CertificateController> logger)
        {
            _authorizeCompanyService = authorizeCompanyService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AuthorizeCompany([FromBody] AuthorizeCompanyRequestDTO authorizeCompanyRequestDTO)
        {
            AuthorizeCompanyResponseDTO response = await _authorizeCompanyService.AuthorizeCompanyAsync(authorizeCompanyRequestDTO);

            return Ok(new
            {
                Success = true,
                Data = response
            });
        }
    }
}
