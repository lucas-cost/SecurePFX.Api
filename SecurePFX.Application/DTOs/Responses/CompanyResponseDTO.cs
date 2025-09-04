namespace SecurePFX.Application.DTOs.Responses
{
    public class CompanyResponseDTO
    {
        public string CompanyName { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public bool IsPayingCustomer { get; set; }
    }
}
