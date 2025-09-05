using AutoMapper;
using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;
using SecurePFX.Domain.Entities;

namespace SecurePFX.Application.Mapping
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CreateCompanyRequestDTO, Company>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Code, opt => opt.MapFrom( src => src.AuthorizationCode));

            CreateMap<Company, CompanyResponseDTO>();
        }
    }
}
