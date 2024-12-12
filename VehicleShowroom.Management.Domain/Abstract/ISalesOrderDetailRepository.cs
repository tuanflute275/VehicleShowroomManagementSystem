using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.Domain.Abstract
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
