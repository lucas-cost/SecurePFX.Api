using AutoMapper;
using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Domain.Entities;

namespace SecurePFX.Application.Mapping
{
    public class AuthorizeCompanyProfile : Profile
    {
        public AuthorizeCompanyProfile()
        {
            CreateMap<AuthorizeCompanyRequestDTO, AuthorizeCompany>()
                .ForMember(dest => dest.CompanyCnpj, opt => opt.MapFrom(src => src.AuthorizedCompanyCnpj))
                .ForMember(dest => dest.CompanyCode, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(_ => DateTime.UtcNow.AddYears(1)))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<AuthorizeCompany, DTOs.Responses.AuthorizeCompanyResponseDTO>()
                .ForMember(dest => dest.AuthorizationCode, opt => opt.MapFrom(src => src.CompanyCode))
                .ForMember(dest => dest.IsAuthorized, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(_ => "Empresa autorizada com sucesso."));
        }
    }
}
