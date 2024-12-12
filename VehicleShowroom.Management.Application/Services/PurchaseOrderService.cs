using AutoMapper;
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

        public async Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(PurchaseOrderViewModel model)
        {
            try
            {
                var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(model.VehicleId);
                if (vehicle == null)return (false, "vehicle do not exit");
                
                var purchaseOrder = new PurchaseOrder(); 
                if (model.PurchaseOrderId == 0)
                {
                    purchaseOrder.SupplierId = model.SupplierId;
                    purchaseOrder.OrderDate = DateTime.Now;
                    purchaseOrder.TotalAmount = 0;
                    bool result = await _unitOfWork.PurchaseOrderRepository.SaveOrUpdateAsync(purchaseOrder);
                    await _unitOfWork.SaveChangeAsync();
                    // PurchaseOrderDetail
                    var purchaseOrderDetail = new PurchaseOrderDetail();
                    purchaseOrderDetail.PurchaseOrderId = purchaseOrder.PurchaseOrderId;
                    purchaseOrderDetail.VehicleId = model.VehicleId;
                    purchaseOrderDetail.Quantity = model.Quantity;
                    purchaseOrderDetail.UnitPrice = (model.Quantity * vehicle.Price);
                    bool result1 = await _unitOfWork.PurchaseOrderDetailRepository.SaveOrUpdateAsync(purchaseOrderDetail);
                    //
                    purchaseOrder = await _unitOfWork.PurchaseOrderRepository.GetByIdAsync(purchaseOrder.PurchaseOrderId);
                    purchaseOrder.TotalAmount = purchaseOrderDetail.UnitPrice;
                    await _unitOfWork.PurchaseOrderRepository.SaveOrUpdateAsync(purchaseOrder);

                    //Stock
                    var stock = new StockHistory();
                    var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");
                    stock.VehicleId = model.VehicleId;
                    stock.ChangeDate = DateTime.Now;

                    if (userIdClaim != null)
                    {
                        // Chuyển đổi sang kiểu int (hoặc kiểu dữ liệu phù hợp)
                        int userId = int.Parse(userIdClaim.Value);
                        stock.UserId = userId;
                    }
                    stock.Quantity = model.Quantity;
                    stock.ChangeType = Constant.STOCKIN;
                    bool result2 = await _unitOfWork.StockHistoryRepository.SaveOrUpdateAsync(stock);
                }
                else
                {
                    purchaseOrder = await _unitOfWork.PurchaseOrderRepository.GetByIdAsync(model.PurchaseOrderId);
                    if (purchaseOrder == null)
                        return (false, "User not found");
                    purchaseOrder.SupplierId = model.SupplierId;
                    purchaseOrder.UpdateBy = "Admin";
                    purchaseOrder.UpdateDate = DateTime.Now;
                    bool result = await _unitOfWork.PurchaseOrderRepository.SaveOrUpdateAsync(purchaseOrder);
                }
                
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
