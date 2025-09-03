using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurePFX.Application.DTOs.Requests;

namespace SecurePFX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyAuthorizationController : ControllerBase
    {
        private readonly ILogger<CompanyAuthorizationController> _logger;

        public CompanyAuthorizationController(ILogger<CompanyAuthorizationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AuthorizeCompany(AuthorizeCompanyRequestDTO authorizeCompanyRequestDTO)
        {

            return Ok("SecurePFX Api is running...");
        }
    }
}
