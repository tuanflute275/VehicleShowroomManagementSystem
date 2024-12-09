using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Mangement.DataAccess.DataAccess;
using VehicleShowroom.Mangement.Domain.Abstract;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.DataAccess.Repository
{
    internal class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync(Expression<Func<Supplier, bool>> expression = null,
          Func<IQueryable<Supplier>, IIncludableQueryable<Supplier, object>>? include = null)
        {
            return await base.GetAllAsync(expression, include);
        }

        public async Task<Supplier?> GetSupplierByIdAsync(int id)
        {
            return await base.GetSingleAsync(x => x.SupplierId == id);
        }

        public async Task<Supplier?> GetSupplierByNameAsync(string name)
        {
            return await base.GetSingleAsync(x => x.SupplierName == name);
        }

        public async Task<bool> SaveOrUpdateAsync(Supplier supplier)
        {
            try
            {
                if (supplier.SupplierId == 0)
                {
                    await base.AddAsync(supplier);
                }
                else
                {
                    base.UpdateAsync(supplier);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Supplier supplier)
        {
            try
            {
                base.DeleteAsync(supplier);
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
