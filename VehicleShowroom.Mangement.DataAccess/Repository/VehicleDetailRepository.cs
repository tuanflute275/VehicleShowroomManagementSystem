
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Mangement.DataAccess.DataAccess;
using VehicleShowroom.Mangement.Domain.Abstract;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.DataAccess.Repository
{
    public class VehicleDetailRepository : BaseRepository<VehicleDetail>, IVehicleDetailRepository
    {
        public VehicleDetailRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<VehicleDetail>> GetAllAsync(Expression<Func<VehicleDetail, bool>> expression = null, Func<IQueryable<VehicleDetail>, IIncludableQueryable<VehicleDetail, object>>? include = null)
        {
            return await base.GetAllAsync(expression, include);
        }

        public async Task<VehicleDetail?> GetByIdAsync(int id)
        {
            return await base.GetSingleAsync(x => x.VehicleDetailId == id);
        } 

        public async Task<bool> SaveOrUpdateAsync(VehicleDetail vehicleDetail)
        {
            try
            {
                if (vehicleDetail.VehicleDetailId == 0)
                {
                    await base.AddAsync(vehicleDetail);
                } else
                {
                    base.UpdateAsync(vehicleDetail);
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(VehicleDetail vehicleDetail)
        {
            try
            {
                base.DeleteAsync(vehicleDetail);    
                await base.Commit();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
