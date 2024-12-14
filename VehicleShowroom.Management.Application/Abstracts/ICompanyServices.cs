using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Domain.Entities;
using X.PagedList;

namespace VehicleShowroom.Management.Application.Abstracts
{
    public interface ICompanyServices
    {
        Task<IPagedList<CompanyDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8);
        Task<bool> DeleteAsync(int id);
        Task<Company> GetByIdAsync(int id);
        Task<bool> SaveOrUpdateAsync(CompanyViewModel model);
    }
}
