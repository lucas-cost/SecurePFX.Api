using AutoMapper;
using SecurePFX.Application.DTOs.Responses;
using SecurePFX.Application.Messages;

namespace SecurePFX.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CertificateResponseDTO, CertificateUploadedMessageEvent>()
                .ForMember(dest => dest.CertificateId, opt => opt.MapFrom(src => src.Id));

        }
    }
}
