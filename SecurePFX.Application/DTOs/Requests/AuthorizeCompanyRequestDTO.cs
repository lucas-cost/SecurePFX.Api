using System.ComponentModel.DataAnnotations;

namespace SecurePFX.Application.DTOs.Requests
{
    public class AuthorizeCompanyRequestDTO
    {
        [Required]
        public string CompanyCnpj { get; set; }

        [Required]
        public Guid CompanyCode { get; set; }

        [Required]
        public string AuthorizedCompanyCnpj { get; set; }
    }
}
