﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PurchaseOrderService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<PurchaseOrderDetailDTO> GetDataByIdAsync(int id)
        {
            var query = _unitOfWork.PurchaseOrderDetailRepository.GetAllAsync(
               expression: s => s.PurchaseOrderId == id,
               include: query => query.Include(x => x.Vehicle).ThenInclude(p => p.Supplier).Include(x => x.PurchaseOrder)
            );
            var dataQuery = await query;
            var dataList = dataQuery.ToList();
            PurchaseOrderDetailDTO data = new PurchaseOrderDetailDTO();
            var listVehicle = new List<VehiclePurchase>();
            foreach (var item in dataList)
            {
                var vehicle = new VehiclePurchase();
                vehicle.VehicleName = item.Vehicle.Name;
                vehicle.Image = item.Vehicle.Image;
                vehicle.ModelNumber = item.Vehicle.ModelNumber;
                vehicle.Quantity = (int)item.Quantity;
                vehicle.UnitPrice = (decimal)item.UnitPrice;
                listVehicle.Add(vehicle);
            }

            data.ListVehicle = listVehicle;
            data.SupplierId = dataList.FirstOrDefault().Vehicle.Supplier.SupplierId;
            data.SupplierName = dataList.FirstOrDefault().Vehicle.Supplier.SupplierName;
            data.SupplierPhone = dataList.FirstOrDefault().Vehicle.Supplier.PhoneNumber;
            data.SupplierEmail = dataList.FirstOrDefault().Vehicle.Supplier.Email;
            data.OrderDate = dataList.FirstOrDefault().PurchaseOrder.OrderDate;
            data.TotalAmount = dataList.FirstOrDefault().PurchaseOrder.TotalAmount;


            return data;
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            try
            {
                var purchase = await _unitOfWork.PurchaseOrderRepository.GetByIdAsync(id);
                if (purchase == null) return (false, "Purchase order not found.");
                await _unitOfWork.PurchaseOrderRepository.DeleteAsync(purchase);
                await _unitOfWork.SaveChangeAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(PurchaseOrderViewModel model)
        {
            try
            {
                var purchaseOrder = new PurchaseOrder();
                decimal totalAmount = 0;
                if (model.PurchaseOrderId == 0)
                {
                    purchaseOrder.SupplierId = model.SupplierId;
                    purchaseOrder.OrderDate = DateTime.Now;
                    purchaseOrder.TotalAmount = 0;
                    bool result = await _unitOfWork.PurchaseOrderRepository.SaveOrUpdateAsync(purchaseOrder);
                    await _unitOfWork.SaveChangeAsync();


                    foreach (var detail in model.Details)
                    {
                        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(detail.VehicleId);
                        if (vehicle == null) return (false, $"Vehicle with ID {detail.VehicleId} does not exist.");

                        // Tạo PurchaseOrderDetail
                        var purchaseOrderDetail = new PurchaseOrderDetail
                        {
                            PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                            VehicleId = detail.VehicleId,
                            Quantity = detail.Quantity,
                            UnitPrice = vehicle.Price * detail.Quantity
                        };

                        // Lưu chi tiết vào database
                        bool resultDetail = await _unitOfWork.PurchaseOrderDetailRepository.SaveOrUpdateAsync(purchaseOrderDetail);
                        if (!resultDetail) return (false, "Failed to save purchase order detail.");

                        // Tính tổng tiền cho PurchaseOrder
                        totalAmount += (decimal)purchaseOrderDetail.UnitPrice;

                        // Tạo StockHistory cho xe
                        var stock = new StockHistory
                        {
                            VehicleId = detail.VehicleId,
                            ChangeDate = DateTime.Now,
                            Quantity = detail.Quantity,
                            ChangeType = Constant.STOCKIN
                        };

                        // Lấy UserId từ Claims
                        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");
                        if (userIdClaim != null)
                        {
                            stock.UserId = int.Parse(userIdClaim.Value);
                        }

                        // Lưu StockHistory vào database
                        bool resultStock = await _unitOfWork.StockHistoryRepository.SaveOrUpdateAsync(stock);
                        if (!resultStock) return (false, "Failed to save stock history.");
                    }
                    purchaseOrder.TotalAmount = totalAmount;
                    bool resultUpdate = await _unitOfWork.PurchaseOrderRepository.SaveOrUpdateAsync(purchaseOrder);
                    if (!resultUpdate) return (false, "Failed to update purchase order total amount.");
                }

                else
                {
                    purchaseOrder = await _unitOfWork.PurchaseOrderRepository.GetByIdAsync(model.PurchaseOrderId);
                    if (purchaseOrder == null) return (false, "purchaseOrder not found");
                    purchaseOrder.SupplierId = model.SupplierId;
                    purchaseOrder.UpdateBy = "Admin";
                    purchaseOrder.UpdateDate = DateTime.Now;
                    bool result = await _unitOfWork.PurchaseOrderRepository.SaveOrUpdateAsync(purchaseOrder);
                    if (!result) return (false, "Failed to update purchase order.");
                }

                await _unitOfWork.SaveChangeAsync();
                return (true, "Purchase order created successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
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

        public async Task<PurchaseOrderViewModel> GetByIdAsync(int id)
        {
            var query = await _unitOfWork.PurchaseOrderRepository.GetByIdAsync(id);

            var data = _mapper.Map<PurchaseOrderViewModel>(query);

            return data;
        }
    }
}
