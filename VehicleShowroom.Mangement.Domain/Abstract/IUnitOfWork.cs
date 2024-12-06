﻿using Microsoft.EntityFrameworkCore;

namespace VehicleShowroom.Mangement.Domain.Abstract
{
    public interface IUnitOfWork
    {
        ISupplierRepository SupplierRepository { get; }
        // other repository


        Task BeginTransaction();
        Task SaveChangeAsync();
        Task CommitTransactionAsync();
        void Dispose();
        Task RollbackTransactionAsync();
        DbSet<T> Table<T>() where T : class;
    }
}
