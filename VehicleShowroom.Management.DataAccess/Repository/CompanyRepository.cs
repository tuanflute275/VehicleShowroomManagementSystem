﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.DataAccess.DataAccess;
using VehicleShowroom.Management.Domain.Abstract;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.DataAccess.Repository
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Company>> GetAllAsync(Expression<Func<Company, bool>> expression = null, Func<IQueryable<Company>, IIncludableQueryable<Company, object>>? include = null)
        {
            return await base.GetAllAsync(expression, include);
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await base.GetSingleAsync(x => x.CompanyId == id);
        }

        public async Task<Company?> GetByNameAsync(string name)
        {
            return await base.GetSingleAsync(x => x.CompanyName == name);
        }

        public async Task<bool> SaveOrUpdateAsync(Company company)
        {
            try
            {
                if (company.CompanyId == 0)
                {
                    await base.AddAsync(company);
                }
                else
                {
                    base.UpdateAsync(company);
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
                await base.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> CountAsync()
        {
            return await base.CountAsync();
        }
    }
}
