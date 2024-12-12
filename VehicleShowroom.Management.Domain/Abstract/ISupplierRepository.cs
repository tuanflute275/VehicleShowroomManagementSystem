
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.Domain.Abstract
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllAsync(Expression<Func<Supplier, bool>> expression = null,
           Func<IQueryable<Supplier>, IIncludableQueryable<Supplier, object>>? include = null);
        Task<Supplier?> GetSupplierByIdAsync(int id);
        Task<Supplier?> GetSupplierByNameAsync(string name);
        Task<bool> SaveOrUpdateAsync(Supplier supplier);
        Task<bool> DeleteAsync(Supplier supplier);
        Task<int> CountAsync();
    }
}
