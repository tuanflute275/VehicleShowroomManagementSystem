using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.Domain.Abstract
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAllAsync(Expression<Func<Vehicle, bool>> expression = null,
           Func<IQueryable<Vehicle>, IIncludableQueryable<Vehicle, object>>? include = null);
        Task<Vehicle?> GetByIdAsync(int id);
        Task<Vehicle?> GetByNameAsync(string name);
        Task<bool> SaveOrUpdateAsync(Vehicle vehicle);
        Task<bool> DeleteAsync(Vehicle vehicle);
    }
}
