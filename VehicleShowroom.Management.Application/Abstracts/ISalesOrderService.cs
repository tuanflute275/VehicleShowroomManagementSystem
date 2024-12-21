using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using X.PagedList;

namespace VehicleShowroom.Management.Application.Abstracts
{
    public interface ISalesOrderService
    {
        Task<IPagedList<SaleOrderDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8);
        Task<List<SalesOrderDetailDTO>> GetByIdAsync(int id);
        Task<SaleOrderViewModel> GetDataByIdAsync(int id);
        Task<SaleOrderExportDTO> GetDataExportByIdAsync(int SalesOrderId);
        Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(SaleOrderViewModel model);
        Task<(bool Success, string ErrorMessage)> DeleteAsync(int id);
        Task<List<UserDTO>> GetAllUserAsync();
        Task<List<VehicleDTO>> GetAllVehicleAsync();
    }
}
