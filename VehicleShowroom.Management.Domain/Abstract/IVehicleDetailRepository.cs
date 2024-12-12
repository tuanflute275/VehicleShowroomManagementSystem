using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.Domain.Abstract
{
    public interface IVehicleDetailRepository
    {
        Task<IEnumerable<VehicleDetail>> GetAllAsync(Expression<Func<VehicleDetail, bool>> expression = null,
           Func<IQueryable<VehicleDetail>, IIncludableQueryable<VehicleDetail, object>>? include = null);
        Task<VehicleDetail?> GetByIdAsync(int id);
        //Task<VehicleDetail?> GetByNameAsync(string name);
        Task<bool> SaveOrUpdateAsync(VehicleDetail vehicleDetail);
        Task<bool> DeleteAsync(VehicleDetail vehicleDetail);
    }
}
