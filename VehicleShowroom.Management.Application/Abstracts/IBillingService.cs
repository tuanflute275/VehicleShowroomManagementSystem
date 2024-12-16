using VehicleShowroom.Management.Application.Models.DTOs;
using X.PagedList;

namespace VehicleShowroom.Management.Application.Abstracts
{
    public interface IBillingService
    {
        Task<IPagedList<BillingDTO>> GetAllPaginationAsync(string? keyword, int page, int pageSize = 8);
        Task<(bool Success, string ErrorMessage)> DeleteAsync(int id);
    }
}
