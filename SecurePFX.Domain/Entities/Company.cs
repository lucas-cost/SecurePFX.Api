using System.ComponentModel.DataAnnotations;

namespace SecurePFX.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string CNPJ { get; set; }

        public string Email { get; set; }

        public bool IsPayingCustomer { get; set; }
    }
}
