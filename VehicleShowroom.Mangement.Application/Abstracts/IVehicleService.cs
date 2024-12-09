using Microsoft.AspNetCore.Http;
using VehicleShowroom.Mangement.Application.Models.DTOs;
using VehicleShowroom.Mangement.Application.Models.ViewModels;
using VehicleShowroom.Mangement.Domain.Entities;
using X.PagedList;

namespace VehicleShowroom.Mangement.Application.Abstracts
{
    public interface IVehicleService
    {
        Task<IPagedList<VehicleDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8);
        Task<List<Supplier>> GetAllSupplierAsync();
        Task<List<Company>> GetAllCompanyAsync();
        Task<Vehicle> GetByIdAsync(int id);
        Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(VehicleViewModel model, IFormFile? fileUpload, string? oldImage = null);
        Task<bool> DeleteAsync(int id);
    }
}
