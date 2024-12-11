using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.Domain.Abstract
{
    public interface IPurchaseOrderRepository
    {
        Task<IEnumerable<PurchaseOrder>> GetAllAsync(Expression<Func<PurchaseOrder, bool>> expression = null,
          Func<IQueryable<PurchaseOrder>, IIncludableQueryable<PurchaseOrder, object>>? include = null);
        Task<PurchaseOrder?> GetByIdAsync(int id);
        Task<bool> SaveOrUpdateAsync(PurchaseOrder purchaseOrder);
        Task<bool> DeleteAsync(PurchaseOrder purchaseOrder);
    }
}
