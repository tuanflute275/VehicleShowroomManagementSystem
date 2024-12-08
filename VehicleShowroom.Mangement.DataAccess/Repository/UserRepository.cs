using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Mangement.DataAccess.DataAccess;
using VehicleShowroom.Mangement.Domain.Abstract;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.DataAccess.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> expression = null,
         Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null)
        {
            return await base.GetAllAsync(expression, include);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await base.GetSingleAsync(x => x.Username == username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await base.GetSingleAsync(x => x.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await base.GetSingleAsync(x => x.UserId == id);
        }

        public async Task<bool> SaveOrUpdateAsync(User user)
        {
            try
            {
                if (user.UserId == 0)
                {
                    await base.AddAsync(user);
                }
                else
                {
                    base.UpdateAsync(user);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(User user)
        {
            try
            {
                base.DeleteAsync(user);
                await base.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
