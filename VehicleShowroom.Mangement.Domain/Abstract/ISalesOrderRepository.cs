using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.Domain.Abstract
{
    public interface ISalesOrderRepository
    {
        Task<IEnumerable<SalesOrder>> GetAllAsync(Expression<Func<SalesOrder, bool>> expression = null,
         Func<IQueryable<SalesOrder>, IIncludableQueryable<SalesOrder, object>>? include = null);
        Task<SalesOrder?> GetByIdAsync(int id);
        Task<bool> SaveOrUpdateAsync(SalesOrder salesOrder);
        Task<bool> DeleteAsync(SalesOrder salesOrder);
    }
}
