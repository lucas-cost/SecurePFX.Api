using AutoMapper;
using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;
using SecurePFX.Domain.Entities;
using System.Security.Cryptography.X509Certificates;

namespace SecurePFX.Application.Mapping
{
    public class CertificateProfile : Profile
    {
        public CertificateProfile()
        {
            CreateMap<CertificateUploadDTO, Certificate>()
                .ForMember(dest => dest.Context, opt => opt.MapFrom(src => src.Context))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive ?? false))
                .ForMember(dest => dest.StorageDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<X509Certificate2, Certificate>()
                .ForMember(dest => dest.Thumbprint, opt => opt.MapFrom(src => src.Thumbprint))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
                .ForMember(dest => dest.Issuer, opt => opt.MapFrom(src => src.Issuer))
                .ForMember(dest => dest.NotBefore, opt => opt.MapFrom(src => src.NotBefore))
                .ForMember(dest => dest.NotAfter, opt => opt.MapFrom(src => src.NotAfter));

            CreateMap<Certificate, CertificateResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Context, opt => opt.MapFrom(src => src.Context))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.NotAfter, opt => opt.MapFrom(src => src.NotAfter))
                .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.StorageDate));
        }
    }
}
