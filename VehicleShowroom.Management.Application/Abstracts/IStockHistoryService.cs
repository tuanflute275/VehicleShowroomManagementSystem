using VehicleShowroom.Management.Application.Models.DTOs;
using X.PagedList;

namespace VehicleShowroom.Management.Application.Abstracts
{
    public interface IStockHistoryService
    {
        Task<List<StockHistoryDTO>> GetAllAsync();
        Task<IPagedList<StockHistoryDTO>> GetAllPaginationAsync(string? keyword, int page, int pageSize = 8);
        Task<StockHistoryDTO> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
