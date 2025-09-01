using System.ComponentModel.DataAnnotations;

namespace SecurePFX.Application.DTOs.Requests
{
    public class CreateCompanyRequestDTO
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", ErrorMessage = "CNPJ inválido. Formato esperado: 00.000.000/0000-00")]
        public string CNPJ { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
    }
}
