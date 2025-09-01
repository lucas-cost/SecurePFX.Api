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
                Company existing = await _unitOfWork.Companies.GetByCnpjAsync(request.CNPJ);

                if (existing != null)
                    throw new InvalidOperationException("Empresa já cadastrada com este CNPJ.");

                //Validação futura para verificar se a companhia esta liberada para de registrar

                Company company = _mapper.Map<Company>(request);

                await _unitOfWork.Companies.CreateAsync(company);

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
