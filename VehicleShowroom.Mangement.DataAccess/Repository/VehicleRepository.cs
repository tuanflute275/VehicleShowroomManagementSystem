using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Mangement.DataAccess.DataAccess;
using VehicleShowroom.Mangement.Domain.Abstract;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.DataAccess.Repository
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync(Expression<Func<Vehicle, bool>> expression = null, Func<IQueryable<Vehicle>, IIncludableQueryable<Vehicle, object>>? include = null)
        {
            return await base.GetAllAsync(expression, include);
        }

        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await base.GetSingleAsync(x => x.VehicleId == id);
        }

        public async Task<Vehicle?> GetByNameAsync(string name)
        {
            return await base.GetSingleAsync(x => x.Name == name);
        }

        public async Task<bool> SaveOrUpdateAsync(Vehicle vehicle)
        {
            try
            {
                if (vehicle.VehicleId == 0)
                {
                    await base.AddAsync(vehicle);
                }
                else
                {
                    base.UpdateAsync(vehicle);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Vehicle vehicle)
        {
            try
            {
                base.DeleteAsync(vehicle);
                await base.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
