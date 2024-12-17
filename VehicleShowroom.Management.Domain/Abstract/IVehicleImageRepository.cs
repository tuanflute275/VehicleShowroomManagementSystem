using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.Domain.Abstract
{
    public interface IVehicleImageRepository
    {
        Task<IEnumerable<VehicleImage>> GetAllAsync(Expression<Func<VehicleImage, bool>> expression = null,
           Func<IQueryable<VehicleImage>, IIncludableQueryable<VehicleImage, object>>? include = null);
        Task<VehicleImage?> GetByIdAsync(int id);
        Task<VehicleImage?> GetDataByIdAsync(int id);
        Task<bool> SaveOrUpdateAsync(VehicleImage vehicleImage);
        Task<bool> DeleteAsync(VehicleImage vehicleImage);
    }
}
