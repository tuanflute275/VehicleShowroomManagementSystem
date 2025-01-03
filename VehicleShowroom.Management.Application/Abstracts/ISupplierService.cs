﻿using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Domain.Entities;
using X.PagedList;

namespace VehicleShowroom.Management.Application.Abstracts
{
    public interface ISupplierService
    {
        Task<IPagedList<SupplierDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8);
        Task<(bool Success, string ErrorMessage)> DeleteAsync(int id);
        Task<Supplier> GetSupplierByIdAsync(int id);
        Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(SupplierViewModel supplierModel);
    }
}
