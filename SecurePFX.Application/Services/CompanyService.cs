using AutoMapper;
using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;
using SecurePFX.Application.Interfaces;
using SecurePFX.Domain.Entities;
using SecurePFX.Domain.Interfaces.UoW;

namespace SecurePFX.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CompanyResponseDTO> CreateCompanyAsync(CreateCompanyRequestDTO request)
        {
            try
            {
                bool existing = await _unitOfWork.Companies.GetByCnpjAsync(request.CNPJ);

                if (existing)
                    throw new InvalidOperationException("Empresa já cadastrada com este CNPJ.");

                AuthorizeCompany? authorizeCompany = await _unitOfWork.AuthorizedCompanies.GetAuthorizedCompany(request.CNPJ, request.AuthorizationCode);

                if (authorizeCompany == null)
                    throw new UnauthorizedAccessException("Empresa não autorizada a se registrar.");

                if (!authorizeCompany.IsActive || authorizeCompany.ExpirationDate < DateTime.UtcNow)
                    throw new UnauthorizedAccessException("Autorização inválida ou expirada.");

                Company company = _mapper.Map<Company>(request);
                await _unitOfWork.Companies.CreateAsync(company);

                await _unitOfWork.CommitAsync();

                authorizeCompany.CompanyId = company.Id;
                await _unitOfWork.AuthorizedCompanies.UpdateAsync(authorizeCompany.Id, authorizeCompany.CompanyId);

                CompanyResponseDTO response = _mapper.Map<CompanyResponseDTO>(company);

                await _unitOfWork.CommitAsync();

                return response;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

    }
}
