using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.Domain.Abstract
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync(Expression<Func<Company, bool>> expression = null,
          Func<IQueryable<Company>, IIncludableQueryable<Company, object>>? include = null);
        Task<Company?> GetByIdAsync(int id);
        Task<Company?> GetByNameAsync(string name);
        Task<bool> SaveOrUpdateAsync(Company company);
        Task<bool> DeleteAsync(Company company);
        Task<int> CountAsync();
    }
}
