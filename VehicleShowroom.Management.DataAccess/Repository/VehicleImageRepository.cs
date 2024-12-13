using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.DataAccess.DataAccess;
using VehicleShowroom.Management.Domain.Abstract;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.DataAccess.Repository
{
    public class VehicleImageRepository : BaseRepository<VehicleImage>, IVehicleImageRepository
    {
        public VehicleImageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<VehicleImage>> GetAllAsync(Expression<Func<VehicleImage, bool>> expression = null,
             Func<IQueryable<VehicleImage>, IIncludableQueryable<VehicleImage, object>>? include = null)
        {
            return await base.GetAllAsync(expression, include);
        }

        public async Task<VehicleImage?> GetByIdAsync(int id)
        {
            return await base.GetSingleAsync(x => x.VehiclelId == id);
        }

        public async Task<bool> SaveOrUpdateAsync(VehicleImage vehicleImage)
        {
            try
            {
                if (vehicleImage.VehicleImageId == 0)
                {
                    await base.AddAsync(vehicleImage);
                }
                else
                {
                    base.UpdateAsync(vehicleImage);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(VehicleImage vehicleImage)
        {
            try
            {
                base.DeleteAsync(vehicleImage);
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
