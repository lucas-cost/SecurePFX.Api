using AutoMapper;
using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;
using SecurePFX.Domain.Entities;

namespace SecurePFX.Application.Mapping
{
    public class CertificateProfile : Profile
    {
        public CertificateProfile()
        {
            CreateMap<CertificateUploadDTO, Certificate>()
                .ForMember(dest => dest.Context, opt => opt.MapFrom(src => src.Context))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive ?? false));

            CreateMap<Certificate, CertificateResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Context, opt => opt.MapFrom(src => src.Context))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.StorageDate));
        }
    }
}
