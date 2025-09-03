using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurePFX.Api.Resources;
using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;
using SecurePFX.Application.Interfaces;

namespace SecurePFX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger, IMapper mapper)
        {
            _companyService = companyService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCompany([FromBody] CreateCompanyRequestDTO request)
        {
            try
            {
                CompanyResponseDTO response = await _companyService.CreateCompanyAsync(request);

                return Ok(new
                {
                    Success = true,
                    Data = response
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { Success = false, ex.Message });
            }
        }
    }
}
