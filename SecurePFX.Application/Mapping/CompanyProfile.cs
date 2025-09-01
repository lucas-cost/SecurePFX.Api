using AutoMapper;
using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;
using SecurePFX.Domain.Entities;

namespace SecurePFX.Application.Mapping
{
    public class CompanyProfile : Profile
    {
        CompanyProfile()
        {
            CreateMap<CreateCompanyRequestDTO, Company>();
            CreateMap<Company, CompanyResponseDTO>();
        }
    }
}
