using Microsoft.AspNetCore.Http;
using SecurePFX.Application.Exceptions.Resource;
using SecurePFX.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SecurePFX.Application.DTOs.Requests
{
    public class CertificateUploadDTO
    {
        [Required(ErrorMessageResourceName = "SRC001", ErrorMessageResourceType = typeof(SrcMsg))]
        public IFormFile File { get; set; } = default!;

        [EnumDataType(typeof(CertificateContext), ErrorMessageResourceName = "SRC002", ErrorMessageResourceType = typeof(SrcMsg))]
        public CertificateContext Context { get; set; }

        [EnumDataType(typeof(CertificateCategory), ErrorMessageResourceName = "SRC003", ErrorMessageResourceType = typeof(SrcMsg))]
        public CertificateCategory Category { get; set; }

        [Required(ErrorMessageResourceName = "SRC004", ErrorMessageResourceType = typeof(SrcMsg))]
        public bool? IsActive { get; set; }

        public string? Password { get; set; }
    }
}
