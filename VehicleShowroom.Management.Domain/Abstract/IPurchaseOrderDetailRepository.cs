using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.Domain.Abstract
{
    public interface IPurchaseOrderDetailRepository
    {
        Task<IEnumerable<PurchaseOrderDetail>> GetAllAsync(Expression<Func<PurchaseOrderDetail, bool>> expression = null,
         Func<IQueryable<PurchaseOrderDetail>, IIncludableQueryable<PurchaseOrderDetail, object>>? include = null);
        Task<PurchaseOrderDetail?> GetByIdAsync(int id);
        Task<bool> SaveOrUpdateAsync(PurchaseOrderDetail purchaseOrderDetail);
        Task<bool> SaveRangeData(List<PurchaseOrderDetail> listDetail);
        Task<bool> DeleteAsync(PurchaseOrderDetail purchaseOrderDetail);
    }
}
