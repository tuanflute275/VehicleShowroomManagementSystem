using VehicleShowroom.Mangement.Application.Models.DTOs;
using VehicleShowroom.Mangement.Application.Models.ViewModels;
using VehicleShowroom.Mangement.Domain.Entities;
using X.PagedList;

namespace VehicleShowroom.Mangement.Application.Abstracts
{
    public interface ISupplierService
    {
        Task<IPagedList<SupplierDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8);
        Task<bool> DeleteAsync(int id);
        Task<Supplier> GetSupplierByIdAsync(int id);
        Task<bool> SaveOrUpdateAsync(SupplierViewModel supplierModel);
    }
}
