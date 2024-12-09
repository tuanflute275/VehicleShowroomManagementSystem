using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using VehicleShowroom.Mangement.DataAccess.DataAccess;
using VehicleShowroom.Mangement.Domain.Abstract;

namespace VehicleShowroom.Mangement.DataAccess.Repository
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
        ICompanyRepository _companyRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table<T>() where T : class => _context.Set<T>();
        
        // repository
        public ISupplierRepository SupplierRepository => _supplierRepository ??= new SupplierRepository(_context);
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        public IVehicleRepository VehicleRepository => _vehicleRepository ??= new VehicleRepository(_context);
        public ICompanyRepository CompanyRepository => _companyRepository ??= new CompanyRepository(_context);

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
