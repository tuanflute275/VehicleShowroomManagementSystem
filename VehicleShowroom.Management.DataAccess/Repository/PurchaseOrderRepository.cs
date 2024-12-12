using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.DataAccess.DataAccess;
using VehicleShowroom.Management.Domain.Abstract;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.DataAccess.Repository
{
    public class PurchaseOrderRepository : BaseRepository<PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllAsync(Expression<Func<PurchaseOrder, bool>> expression = null, 
            Func<IQueryable<PurchaseOrder>, IIncludableQueryable<PurchaseOrder, object>>? include = null)
        {
            return await base.GetAllAsync(expression, include);
        }

        public async Task<PurchaseOrder?> GetByIdAsync(int id)
        {
            return await base.GetSingleAsync(x => x.PurchaseOrderId == id);
        }

        public async Task<bool> SaveOrUpdateAsync(PurchaseOrder purchaseOrder)
        {
            try
            {
                if (purchaseOrder.PurchaseOrderId == 0)
                {
                    await base.AddAsync(purchaseOrder);
                }
                else
                {
                    base.UpdateAsync(purchaseOrder);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(PurchaseOrder purchaseOrder)
        {
            try
            {
                base.DeleteAsync(purchaseOrder);
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
