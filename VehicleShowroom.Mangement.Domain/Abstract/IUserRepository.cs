using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.Domain.Abstract
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> expression = null,
           Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> SaveOrUpdateAsync(User user);
        Task<bool> DeleteAsync(User user);
    }
}
