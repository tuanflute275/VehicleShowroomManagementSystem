using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.DataAccess.DataAccess;
using VehicleShowroom.Management.Domain.Abstract;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.DataAccess.Repository
{
    public class SalesOrderRepository : BaseRepository<SalesOrder>, ISalesOrderRepository
    {
        public SalesOrderRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<SalesOrder>> GetAllAsync(Expression<Func<SalesOrder, bool>> expression = null,
           Func<IQueryable<SalesOrder>, IIncludableQueryable<SalesOrder, object>>? include = null)
        {
            return await base.GetAllAsync(expression, include);
        }

        public async Task<SalesOrder?> GetByIdAsync(int id)
        {
            return await base.GetSingleAsync(x => x.SalesOrderId == id);
        }

        public async Task<bool> SaveOrUpdateAsync(SalesOrder salesOrder)
        {
            try
            {
                if (salesOrder.SalesOrderId == 0)
                {
                    await base.AddAsync(salesOrder);
                }
                else
                {
                    base.UpdateAsync(salesOrder);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(SalesOrder salesOrder)
        {
            try
            {
                base.DeleteAsync(salesOrder);
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
