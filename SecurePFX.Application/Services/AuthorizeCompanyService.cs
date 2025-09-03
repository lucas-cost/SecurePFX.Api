using AutoMapper;
using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;
using SecurePFX.Application.Interfaces;
using SecurePFX.Domain.Entities;
using SecurePFX.Domain.Interfaces.UoW;

namespace SecurePFX.Application.Services
{
    public class AuthorizeCompanyService : IAuthorizeCompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorizeCompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthorizeCompanyResponseDTO> AuthorizeCompanyAsync(AuthorizeCompanyRequestDTO dto)
        {
            try
            {
                bool authorizeResult = await _unitOfWork.AuthorizedCompanies.GetCompanyByCnpjAndCodeAsync(dto.CompanyCnpj, dto.CompanyCode);

                bool companyResult = await _unitOfWork.AuthorizedCompanies.GetCompanyByCnpjAsync(dto.AuthorizedCompanyCnpj);

                if (!authorizeResult)
                    throw new UnauthorizedAccessException("Empresa inválida ou não autorizadora.");

                if (companyResult)
                    throw new InvalidOperationException("Empresa já autorizada.");

                AuthorizeCompany authorizeCompany = _mapper.Map<AuthorizeCompany>(dto);

                await _unitOfWork.AuthorizedCompanies.CreateAsync(authorizeCompany);

                AuthorizeCompanyResponseDTO response = _mapper.Map<AuthorizeCompanyResponseDTO>(authorizeCompany);

                await _unitOfWork.CommitAsync();

                return response;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw ex;
            }
        }
    }
}
