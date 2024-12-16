using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using VehicleShowroom.Management.DataAccess.DataAccess;
using VehicleShowroom.Management.Domain.Abstract;

namespace VehicleShowroom.Management.DataAccess.Repository
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private bool disposedValue;
        ApplicationDbContext _context;
        IDbContextTransaction _dbContextTransaction;
        // DI repository
        ISupplierRepository _supplierRepository;
        IUserRepository _userRepository;
        IVehicleRepository _vehicleRepository;
        IVehicleImageRepository _vehicleImageRepository;
        ICompanyRepository _companyRepository;
        IPurchaseOrderRepository _purchaseOrderRepository;
        IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;
        ISalesOrderRepository _salesOrderRepository;
        ISalesOrderDetailRepository _salesOrderDetailRepository;
        IStockHistoryRepository _stockHistoryRepository;
        IBillingRepository _billingRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table<T>() where T : class => _context.Set<T>();
        
        // repository
        public ISupplierRepository SupplierRepository => _supplierRepository ??= new SupplierRepository(_context);
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        public IVehicleRepository VehicleRepository => _vehicleRepository ??= new VehicleRepository(_context);
        public IVehicleImageRepository VehicleImageRepository => _vehicleImageRepository ??= new VehicleImageRepository(_context);
        public ICompanyRepository CompanyRepository => _companyRepository ??= new CompanyRepository(_context);
        public IPurchaseOrderRepository PurchaseOrderRepository => _purchaseOrderRepository ??= new PurchaseOrderRepository(_context);
        public IPurchaseOrderDetailRepository PurchaseOrderDetailRepository => _purchaseOrderDetailRepository ??= new PurchaseOrderDetailRepository(_context);
        public ISalesOrderRepository SalesOrderRepository => _salesOrderRepository ??= new SalesOrderRepository(_context);
        public ISalesOrderDetailRepository SalesOrderDetailRepository => _salesOrderDetailRepository ??=new SalesOrderDetailRepository(_context);
        public IStockHistoryRepository StockHistoryRepository => _stockHistoryRepository ??= new StockHistoryRepository(_context);
        public IBillingRepository BillingRepository => _billingRepository ??= new BillingRepository(_context);
        
        public async Task BeginTransaction()
        {
            _dbContextTransaction = await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            await _dbContextTransaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContextTransaction.RollbackAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
