
using Microsoft.AspNetCore.Http;
using VehicleShowroom.Mangement.Application.Models.DTOs;
using VehicleShowroom.Mangement.Application.Models.ViewModels;
using VehicleShowroom.Mangement.Domain.Entities;
using X.PagedList;

namespace VehicleShowroom.Mangement.Application.Abstracts
{
    public interface IVehicleDetailServices
    {
        Task<IPagedList<VehicleDetailDTO>> GetAllPaginationAsync(int page, int pageSize = 8);
        Task<List<Vehicle>> GetAllVehicleAsync();
        Task<VehicleDetail> GetByIdAsync(int id);
        Task<bool> SaveOrUpdateAsync(VehicleDetailViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}
