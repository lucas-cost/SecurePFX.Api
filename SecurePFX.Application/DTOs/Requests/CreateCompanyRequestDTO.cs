using System.ComponentModel.DataAnnotations;

namespace SecurePFX.Application.DTOs.Requests
{
    public class CreateCompanyRequestDTO
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string CNPJ { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Required]
        public Guid AuthorizationCode { get; set; }
    }
}
