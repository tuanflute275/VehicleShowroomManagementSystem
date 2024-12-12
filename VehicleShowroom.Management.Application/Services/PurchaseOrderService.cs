using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Application.Utils;
using VehicleShowroom.Management.Domain.Abstract;
using VehicleShowroom.Management.Domain.Entities;
using X.PagedList;

namespace VehicleShowroom.Management.Application.Services
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

        public async Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(PurchaseOrderViewModel model )
        {
            try
            {
                var purchaseOrder = new PurchaseOrder();
                if (model.PurchaseOrderId == 0)
                {
                    purchaseOrder.SupplierId = model.SupplierId;
                    purchaseOrder.OrderDate = DateTime.Now;
                }
                else
                {
                    purchaseOrder = await _unitOfWork.PurchaseOrderRepository.GetByIdAsync(model.PurchaseOrderId);
                    if (purchaseOrder == null)
                        return (false, "User not found");

                    purchaseOrder.SupplierId = model.SupplierId;
                    purchaseOrder.OrderDate = DateTime.Now;
                   
                }
                bool result = await _unitOfWork.PurchaseOrderRepository.SaveOrUpdateAsync(purchaseOrder);
                await _unitOfWork.SaveChangeAsync();
                return (true, null);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
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
