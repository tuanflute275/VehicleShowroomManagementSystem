using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using X.PagedList;

namespace VehicleShowroom.Management.Application.Abstracts
{
    public interface ISalesOrderService
    {
        Task<IPagedList<SaleOrderDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8);
        Task<List<SalesOrderDetailDTO>> GetByIdAsync(int id);
        Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(SaleOrderViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}
