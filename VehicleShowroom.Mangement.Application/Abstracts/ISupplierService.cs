using VehicleShowroom.Mangement.Application.Models.DTOs;
using VehicleShowroom.Mangement.Application.Models.DTOs.Supplier;
using VehicleShowroom.Mangement.Application.Models.ViewModels;

namespace VehicleShowroom.Mangement.Application.Abstracts
{
    public interface ISupplierService
    {
        Task<ResponseDatatable<SupplierDTO>> GetSupplierByPaginationAsync(RequestDatatable request);
        Task<bool> DeleteAsync(int id);
        Task<SupplierViewModel> GetSupplierByIdAsync(int id);
        Task<ResponseModel> SaveAsync(SupplierViewModel supplierModel);
    }
}
