using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using X.PagedList;

namespace VehicleShowroom.Management.Application.Abstracts
{
    public interface IPurchaseOrderService
    {
        Task<List<SupplierDTO>> GetAllSupplierAsync();
        Task<List<VehicleDTO>> GetAllVehicleAsync();
        // PurchaseOrder
        Task<IPagedList<PurchaseOrderDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8);
        Task<PurchaseOrderDetailDTO> GetDataByIdAsync(int id);
        Task<PurchaseOrderViewModel> GetByIdAsync(int id);
        Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(PurchaseOrderViewModel model);
        Task<(bool Success, string ErrorMessage)> DeleteAsync(int id);

        // PurchaseOrderDetail
    }
}
