namespace SecurePFX.Domain.Entities
{
    public class AuthorizeCompany
    {
        public int Id { get; set; }

        public string CompanyCnpj { get; set; }

        public Guid CompanyCode { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }    

        public DateTime ExpirationDate { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
