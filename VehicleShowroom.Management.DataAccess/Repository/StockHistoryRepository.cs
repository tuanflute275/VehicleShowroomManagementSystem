using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.DataAccess.DataAccess;
using VehicleShowroom.Management.Domain.Abstract;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.DataAccess.Repository
{
    public class StockHistoryRepository : BaseRepository<StockHistory>, IStockHistoryRepository
    {
        public StockHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<StockHistory>> GetAllAsync(Expression<Func<StockHistory, bool>> expression = null,
            Func<IQueryable<StockHistory>, IIncludableQueryable<StockHistory, object>>? include = null)
        {
            return await base.GetAllAsync(expression, include);
        }

        public async Task<StockHistory?> GetByIdAsync(int id)
        {
            return await base.GetSingleAsync(x => x.StockHistoryId == id);
        }

        public async Task<bool> SaveOrUpdateAsync(StockHistory stockHistory)
        {
            try
            {
                if (stockHistory.StockHistoryId == 0)
                {
                    await base.AddAsync(stockHistory);
                }
                else
                {
                    base.UpdateAsync(stockHistory);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(StockHistory stockHistory)
        {
            try
            {
                base.DeleteAsync(stockHistory);
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
