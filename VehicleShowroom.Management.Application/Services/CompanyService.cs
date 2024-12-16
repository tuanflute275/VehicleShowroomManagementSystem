using AutoMapper;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Domain.Abstract;
using VehicleShowroom.Management.Domain.Entities;
using X.PagedList;

namespace VehicleShowroom.Management.Application.Services
{
    public class CompanyService : ICompanyServices
    {
        IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedList<CompanyDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8)
        {
            var companyQuery = _unitOfWork.CompanyRepository.GetAllAsync(
                 expression: s => string.IsNullOrEmpty(keyword) || s.CompanyName.Contains(keyword)
             );
            var company = await companyQuery;
            var companyList = company.ToList();
            var data = _mapper.Map<List<CompanyDTO>>(companyList);
            return data.ToPagedList(page, pageSize);
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _unitOfWork.CompanyRepository.GetByIdAsync(id);
        }

        public async Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(CompanyViewModel model)
        {
            try
            {
                var company = new Company();
                if (model.CompanyId == 0)
                {
                    company.CompanyName = model.CompanyName;
                    company.PhoneNumber = model.PhoneNumber;
                    company.Email = model.Email;
                    company.Status = model.Status;
                    company.CreateBy = "Admin";
                    company.CreateDate = DateTime.Now;
                }
                else
                {
                    company = await _unitOfWork.CompanyRepository.GetByIdAsync(model.CompanyId);
                    if (company == null) return (false, "Company not found.");
                    company.CompanyName = model.CompanyName;
                    company.PhoneNumber = model.PhoneNumber;
                    company.Email = model.Email;
                    company.Status = model.Status;
                    company.UpdateBy = "Admin";
                    company.UpdateDate = DateTime.Now;
                }
                bool result = await _unitOfWork.CompanyRepository.SaveOrUpdateAsync(company);
                if (!result) return (false, "An error occurred while saving the company.");
                await _unitOfWork.SaveChangeAsync();
                return (true, null);
            }
            catch (Exception e)
            {
                return (false, $"An error occurred: {e.Message}");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            try
            {
                var company = await _unitOfWork.CompanyRepository.GetByIdAsync(id);
                if (company == null) return (false, "Company not found.");
                await _unitOfWork.CompanyRepository.DeleteAsync(company);
                await _unitOfWork.SaveChangeAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }
    }
}
