using Microsoft.EntityFrameworkCore;

namespace VehicleShowroom.Management.Domain.Abstract
{
    public interface IUnitOfWork
    {
        ISupplierRepository SupplierRepository { get; }
        IUserRepository UserRepository { get; }
        IVehicleRepository VehicleRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IPurchaseOrderRepository PurchaseOrderRepository { get; }
        IPurchaseOrderDetailRepository PurchaseOrderDetailRepository { get; }
        ISalesOrderRepository SalesOrderRepository { get; }
        ISalesOrderDetailRepository SalesOrderDetailRepository { get; }
        IStockHistoryRepository StockHistoryRepository { get; }
        // other repository

        Task BeginTransaction();
        Task SaveChangeAsync();
        Task CommitTransactionAsync();
        void Dispose();
        Task RollbackTransactionAsync();
        DbSet<T> Table<T>() where T : class;
    }
}
