using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.DataAccess.DataAccess;
using VehicleShowroom.Management.Domain.Abstract;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.DataAccess.Repository
{
    public class BillingRepository : BaseRepository<Billing>, IBillingRepository
    {
        public BillingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Billing>> GetAllAsync(Expression<Func<Billing, bool>> expression = null, Func<IQueryable<Billing>, IIncludableQueryable<Billing, object>>? include = null)
        {
            return await base.GetAllAsync(expression, include);
        }

        public async Task<Billing?> GetByIdAsync(int id)
        {
            return await base.GetSingleAsync(x => x.BillingId == id);
        }

        public async Task<bool> SaveOrUpdateAsync(Billing billing)
        {
            try
            {
                if (billing.BillingId == 0)
                {
                    await base.AddAsync(billing);
                }
                else
                {
                    base.UpdateAsync(billing);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Billing billing)
        {
            try
            {
                base.DeleteAsync(billing);
                await base.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> CountAsync()
        {
            return await base.CountAsync();
        }
    }
}
