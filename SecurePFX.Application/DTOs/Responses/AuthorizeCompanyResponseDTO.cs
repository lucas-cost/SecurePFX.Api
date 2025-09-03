namespace SecurePFX.Application.DTOs.Responses
{
    public class AuthorizeCompanyResponseDTO
    {
        public Guid AuthorizationCode { get; set; }
        public string Message { get; set; }
        public bool IsAuthorized { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}