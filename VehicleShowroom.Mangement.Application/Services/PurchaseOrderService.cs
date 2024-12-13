using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Mangement.Application.Abstracts;
using VehicleShowroom.Mangement.Application.Models.DTOs;
using VehicleShowroom.Mangement.Domain.Abstract;
using X.PagedList;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace VehicleShowroom.Mangement.Application.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PurchaseOrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IPagedList<PurchaseOrderDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8)
        {
            var query = _unitOfWork.PurchaseOrderRepository.GetAllAsync(
                expression: s => string.IsNullOrEmpty(keyword) || s.Supplier.SupplierName.Contains(keyword),
                include: query => query.Include(x => x.Supplier)
             );
            var dataQuery = await query; 
            var dataList = dataQuery.ToList();
            var data = _mapper.Map<List<PurchaseOrderDTO>>(dataList);
            return data.ToPagedList(page, pageSize); 
        }

        public async Task<PurchaseOrderDTO> GetByIdAsync(int id)
        {
            var dataQuery = await _unitOfWork.PurchaseOrderRepository.GetByIdAsync(id);
            var data = _mapper.Map<PurchaseOrderDTO>(dataQuery);
            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var purchase = await _unitOfWork.PurchaseOrderRepository.GetByIdAsync(id);
                if (purchase == null)
                {
                    return false;
                }

                await _unitOfWork.PurchaseOrderRepository.DeleteAsync(purchase);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<SupplierDTO>> GetAllSupplierAsync()
        {
            var supplierQuery = _unitOfWork.SupplierRepository.GetAllAsync();
            var suppliers = await supplierQuery;
            var supplierList = suppliers.ToList();
            var data = _mapper.Map<List<SupplierDTO>>(supplierList);
            return data;
        }

        public async Task<List<VehicleDTO>> GetAllVehicleAsync()
        {
            var vehicleQuery = _unitOfWork.VehicleRepository.GetAllAsync();
            var vehicle = await vehicleQuery;
            var vehicleList = vehicle.ToList();
            var data = _mapper.Map<List<VehicleDTO>>(vehicleList);
            return data;
        }
    }
}
