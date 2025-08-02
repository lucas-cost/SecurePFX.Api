using Microsoft.AspNetCore.Http;
using SecurePFX.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SecurePFX.Application.DTOs.Requests
{
    public class CertificateUploadDTO
    {
        [Required(ErrorMessage = "O arquivo do certificado é obrigatório.")]
        public IFormFile File { get; set; } = default!;

        [EnumDataType(typeof(CertificateContext), ErrorMessage = "Contexto inválido")]
        public CertificateContext Context { get; set; }

        [EnumDataType(typeof(CertificateCategory), ErrorMessage = "Categoria inválida")]
        public CertificateCategory Category { get; set; }

        [Required(ErrorMessage = "O nome do certificado é obrigatório.")]
        public bool? IsActive { get; set; }
    }
}
