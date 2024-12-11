using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.Domain.Abstract
{
    public interface IPurchaseOrderDetailRepository
    {
        Task<IEnumerable<PurchaseOrderDetail>> GetAllAsync(Expression<Func<PurchaseOrderDetail, bool>> expression = null,
         Func<IQueryable<PurchaseOrderDetail>, IIncludableQueryable<PurchaseOrderDetail, object>>? include = null);
        Task<PurchaseOrderDetail?> GetByIdAsync(int id);
        Task<bool> SaveOrUpdateAsync(PurchaseOrderDetail purchaseOrderDetail);
        Task<bool> DeleteAsync(PurchaseOrderDetail purchaseOrderDetail);
    }
}
