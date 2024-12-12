using VehicleShowroom.Management.Application.Models.DTOs;
using X.PagedList;

namespace VehicleShowroom.Management.Application.Abstracts
{
    public interface IPurchaseOrderService
    {
        Task<List<SupplierDTO>> GetAllSupplierAsync();
        Task<List<VehicleDTO>> GetAllVehicleAsync();
        // PurchaseOrder
        Task<IPagedList<PurchaseOrderDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8);
        Task<PurchaseOrderDTO> GetByIdAsync(int id);
        Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync();
        Task<bool> DeleteAsync(int id);

        // PurchaseOrderDetail
    }
}
