using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.DataAccess.DataAccess;
using VehicleShowroom.Management.Domain.Abstract;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.DataAccess.Repository
{
    public class PurchaseOrderDetailRepository : BaseRepository<PurchaseOrderDetail>, IPurchaseOrderDetailRepository
    {
        public PurchaseOrderDetailRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PurchaseOrderDetail>> GetAllAsync(Expression<Func<PurchaseOrderDetail, bool>> expression = null,
          Func<IQueryable<PurchaseOrderDetail>, IIncludableQueryable<PurchaseOrderDetail, object>>? include = null)
        {
            return await base.GetAllAsync(expression, include);
        }

        public async Task<PurchaseOrderDetail?> GetByIdAsync(int id)
        {
            return await base.GetSingleAsync(x => x.PurchaseOrderDetailId == id);
        }

        public async Task<bool> SaveOrUpdateAsync(PurchaseOrderDetail purchaseOrderDetail)
        {
            try
            {
                if (purchaseOrderDetail.PurchaseOrderDetailId == 0)
                {
                    await base.AddAsync(purchaseOrderDetail);
                }
                else
                {
                    base.UpdateAsync(purchaseOrderDetail);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(PurchaseOrderDetail purchaseOrderDetail)
        {
            try
            {
                base.DeleteAsync(purchaseOrderDetail);
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
