
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.Domain.Abstract
{
    public interface IBillingRepository
    {
        Task<IEnumerable<Billing>> GetAllAsync(Expression<Func<Billing, bool>> expression = null,
           Func<IQueryable<Billing>, IIncludableQueryable<Billing, object>>? include = null);
        Task<Billing?> GetByIdAsync(int id);
        Task<bool> SaveOrUpdateAsync(Billing billing);
        Task<bool> DeleteAsync(Billing billing);
        Task<int> CountAsync();
    }
}
