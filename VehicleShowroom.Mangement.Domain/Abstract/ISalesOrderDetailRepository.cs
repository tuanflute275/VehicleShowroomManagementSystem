using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.Domain.Abstract
{
    public interface ISalesOrderDetailRepository
    {
        Task<IEnumerable<SalesOrderDetail>> GetAllAsync(Expression<Func<SalesOrderDetail, bool>> expression = null,
         Func<IQueryable<SalesOrderDetail>, IIncludableQueryable<SalesOrderDetail, object>>? include = null);
        Task<SalesOrderDetail?> GetByIdAsync(int id);
        Task<bool> SaveOrUpdateAsync(SalesOrderDetail salesOrderDetail);
        Task<bool> DeleteAsync(SalesOrderDetail salesOrderDetail);
    }
}
