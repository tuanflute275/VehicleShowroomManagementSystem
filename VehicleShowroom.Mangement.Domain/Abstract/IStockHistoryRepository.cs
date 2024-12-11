using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.Domain.Abstract
{
    public interface IStockHistoryRepository
    {
        Task<IEnumerable<StockHistory>> GetAllAsync(Expression<Func<StockHistory, bool>> expression = null,
         Func<IQueryable<StockHistory>, IIncludableQueryable<StockHistory, object>>? include = null);
        Task<StockHistory?> GetByIdAsync(int id);
        Task<bool> SaveOrUpdateAsync(StockHistory stockHistory);
        Task<bool> DeleteAsync(StockHistory stockHistory);
    }
}
