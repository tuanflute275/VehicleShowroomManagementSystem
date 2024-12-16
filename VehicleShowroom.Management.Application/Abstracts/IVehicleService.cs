using Microsoft.AspNetCore.Http;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Domain.Entities;
using X.PagedList;

namespace VehicleShowroom.Management.Application.Abstracts
{
    public interface IVehicleService
    {
        Task<IPagedList<VehicleDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8);
        Task<List<Supplier>> GetAllSupplierAsync();
        Task<List<Company>> GetAllCompanyAsync();
        Task<Vehicle> GetByIdAsync(int id);
        Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(VehicleViewModel model, IFormFile? fileUpload, string? oldImage = null);
        Task<(bool Success, string ErrorMessage)> DeleteAsync(int id);
        //VehicleImage
        Task<IPagedList<VehicleImageDTO>> GetAllImagePaginationAsync(int vehicleId, int page, int pageSize = 8);
    }
}
