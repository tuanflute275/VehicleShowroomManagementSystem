using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Mangement.DataAccess.DataAccess;
using VehicleShowroom.Mangement.Domain.Abstract;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.DataAccess.Repository
{
    public class CompanyRepositoty : BaseRepository<Company>, ICompanyRepositoty
    {
        public CompanyRepositoty(ApplicationDbContext context) : base(context) { }

        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await GetSingleAsync(x => x.CompanyId == id);
        }

        public async Task<Company?> GetCompanyByNameAsync(string name)
        {
            return await GetSingleAsync(x => x.CompanyName == name);
        }

        public async Task<bool> SaveOrUpdateAsync(Company company)
        {
            try
            {
                if (company.CompanyId == 0)
                {
                    await AddAsync(company);
                }
                else
                {
                    UpdateAsync(company);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Company company)
        {
            try
            {
                base.DeleteAsync(company);
                await Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Company>> GetAllAsync(Expression<Func<Company, bool>> expression = null,
           Func<IQueryable<Company>, IIncludableQueryable<Company, object>>? include = null)
        {
            return await base.GetAllAsync(expression, include);
        }
    }
}
