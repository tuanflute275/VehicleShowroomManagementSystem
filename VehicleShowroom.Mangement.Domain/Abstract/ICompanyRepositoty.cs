using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.Domain.Abstract
{
    public interface ICompanyRepositoty
    {
        Task<IEnumerable<Company>> GetAllAsync(Expression<Func<Company, bool>> expression = null,
           Func<IQueryable<Company>, IIncludableQueryable<Company, object>>? include = null);
        Task<Company?> GetCompanyByIdAsync(int id);
        Task<Company?> GetCompanyByNameAsync(string name);
        Task<bool> SaveOrUpdateAsync(Company company);
        Task<bool> DeleteAsync(Company company);
    }
}
