using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Mangement.DataAccess.DataAccess;
using VehicleShowroom.Mangement.Domain.Abstract;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.DataAccess.Repository
{
    public class SalesOrderDetailRepository : BaseRepository<SalesOrderDetail>, ISalesOrderDetailRepository
    {
        public SalesOrderDetailRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SalesOrderDetail>> GetAllAsync(Expression<Func<SalesOrderDetail, bool>> expression = null,
          Func<IQueryable<SalesOrderDetail>, IIncludableQueryable<SalesOrderDetail, object>>? include = null)
        {
            return await base.GetAllAsync(expression, include);
        }

        public async Task<SalesOrderDetail?> GetByIdAsync(int id)
        {
            return await base.GetSingleAsync(x => x.SalesOrderDetailId == id);
        }

        public async Task<bool> SaveOrUpdateAsync(SalesOrderDetail salesOrderDetail)
        {
            try
            {
                if (salesOrderDetail.SalesOrderDetailId == 0)
                {
                    await base.AddAsync(salesOrderDetail);
                }
                else
                {
                    base.UpdateAsync(salesOrderDetail);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(SalesOrderDetail salesOrderDetail)
        {
            try
            {
                base.DeleteAsync(salesOrderDetail);
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
