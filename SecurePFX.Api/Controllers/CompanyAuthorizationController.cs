using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecurePFX.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyAuthorizationController : ControllerBase
    {
        private readonly ILogger<CompanyAuthorizationController> _logger;

        public CompanyAuthorizationController(ILogger<CompanyAuthorizationController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AuthorizeCompany()
        {
            return Ok("SecurePFX Api is running...");
        }
    }
}
