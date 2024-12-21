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
    public class SalesOrderService : ISalesOrderService
    {
        IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SalesOrderService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
       
        public async Task<IPagedList<SaleOrderDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8)
        {
            var query = _unitOfWork.SalesOrderRepository.GetAllAsync(
                expression: x => string.IsNullOrEmpty(keyword) || x.User.Username.Contains(keyword) 
                || x.User.FullName.Contains(keyword) || x.User.Email.Contains(keyword) || x.User.PhoneNumber.Contains(keyword),
                include: query => query.Include(x => x.User)
             );
            var dataQuery = await query;
            var dataList = dataQuery.ToList();
            var data = _mapper.Map<List<SaleOrderDTO>>(dataList);
            return data.ToPagedList(page, pageSize);
        }

        public async Task<List<SalesOrderDetailDTO>> GetByIdAsync(int id)
        {
            var salesOrderDetails = await _unitOfWork.SalesOrderDetailRepository.GetAllAsync(
                expression: x => x.SalesOrderId == id,
                include: query => query
                    .Include(x => x.Vehicle)
                    .ThenInclude(v => v.Supplier)
                    .Include(x => x.Vehicle.Company)
            );

            // Ánh xạ dữ liệu sang DTO
            var result = salesOrderDetails.Select(detail => new SalesOrderDetailDTO
            {
                Image = detail.Vehicle?.Image,
                VehicleName = detail.Vehicle?.Name,
                ModelNumber = detail.Vehicle?.ModelNumber,
                SupplierName = detail.Vehicle?.Supplier?.SupplierName,
                CompanyName = detail.Vehicle?.Company?.CompanyName,
                Quantity = (int)detail.Quantity,
                UnitPrice = detail.UnitPrice
            }).ToList();

            return result;
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            try
            {
                var sale = await _unitOfWork.SalesOrderRepository.GetByIdAsync(id);
                if (sale == null) return (false, "Sales order not found.");
                await _unitOfWork.SalesOrderRepository.DeleteAsync(sale);
                await _unitOfWork.SaveChangeAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(SaleOrderViewModel model)
        {
            try
            {
                var saleOrder = new SalesOrder();
                decimal totalAmount = 0;
                if(model.SalesOrderId == 0)
                {
                    var checkStock = await _unitOfWork.StockHistoryRepository.GetAllAsync(
                       expression: x => x.ChangeType.Contains(Constant.STOCKIN) && x.VehicleId == model.VehicleId
                     );
                    if (!checkStock.Any()) return (false, "Stock not found");
                    
                    var stockQuantity = checkStock.Sum(x => x.Quantity);
                    var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync((int)model.VehicleId);
                    totalAmount = (decimal)vehicle.Price;
                    if (stockQuantity <= 0)
                    {
                        saleOrder.UserId = model.UserId;
                        saleOrder.OrderDate = DateTime.Now;
                        saleOrder.TotalAmount = totalAmount;
                        saleOrder.Status = Constant.PENDING;
                        bool resultNotInStock = await _unitOfWork.SalesOrderRepository.SaveOrUpdateAsync(saleOrder);
                        await _unitOfWork.SaveChangeAsync();

                        //save detail
                        var saleOrderDetail = new SalesOrderDetail
                        {
                            SalesOrderId = saleOrder.SalesOrderId,
                            VehicleId = (int)model.VehicleId,
                            Quantity = 1,
                            UnitPrice = (decimal)vehicle.Price
                        };
                        // save detail database
                        bool resultDetail = await _unitOfWork.SalesOrderDetailRepository.SaveOrUpdateAsync(saleOrderDetail);
                        if (!resultDetail) return (false, "Failed to save sale order detail.");

                        //save data billing
                        var billing = new Billing
                        {
                            SaleOrderId = saleOrder.SalesOrderId,
                            UserId = model.UserId,
                            BillingDate = DateTime.Now,
                            Amount = totalAmount,
                            PaymentMethod = model.PaymentMethod,
                            Status = Constant.PENDING,
                        };
                        if (string.IsNullOrEmpty(model.Notes))
                            billing.Notes = Constant.NOTE_AWAIT;
                        else 
                            billing.Notes = model.Notes;
                        
                        bool resultBilling = await _unitOfWork.BillingRepository.SaveOrUpdateAsync(billing);
                        if (!resultBilling) return (false, "Failed to update billing quantity.");
                    }
                    else
                    {
                        saleOrder.UserId = model.UserId;
                        saleOrder.OrderDate = DateTime.Now;
                        saleOrder.TotalAmount = totalAmount;
                        saleOrder.Status = Constant.COMPLETED;
                        bool result = await _unitOfWork.SalesOrderRepository.SaveOrUpdateAsync(saleOrder);
                        await _unitOfWork.SaveChangeAsync();

                        //save detail
                        var saleOrderDetail = new SalesOrderDetail
                        {
                            SalesOrderId = saleOrder.SalesOrderId,
                            VehicleId = (int)model.VehicleId,
                            Quantity = 1,
                            UnitPrice = (decimal)vehicle.Price
                        };
                        // save detail database
                        bool resultDetail = await _unitOfWork.SalesOrderDetailRepository.SaveOrUpdateAsync(saleOrderDetail);
                        if (!resultDetail) return (false, "Failed to save sale order detail.");

                        // Create StockHistory for the sale (stock out)
                        var stock = new StockHistory
                        {
                            VehicleId = (int)model.VehicleId,
                            ChangeDate = DateTime.Now,
                            Quantity = 1,
                            ChangeType = Constant.STOCKOUT
                        };
                        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");
                        if (userIdClaim != null)
                        {
                            stock.UserId = int.Parse(userIdClaim.Value);
                        }
                        // Lưu StockHistory vào database
                        bool resultStock = await _unitOfWork.StockHistoryRepository.SaveOrUpdateAsync(stock);
                        if (!resultStock) return (false, "Failed to save stock history.");

                        // Update the stock quantity in the database to reflect the sale
                        var stockRecord = await _unitOfWork.StockHistoryRepository.GetAllAsync(
                            expression: x =>  x.VehicleId == model.VehicleId
                         ); // Assume StockRepository is available
                        if (stockRecord != null && stockRecord.Any())
                        {
                            // Get the most recent stock record (assuming the stock list is ordered by ChangeDate in descending order)
                            var latestStock = stockRecord.OrderByDescending(x => x.ChangeDate).FirstOrDefault();
                            // Ensure that we have enough stock available
                            if (latestStock != null)
                            {
                                latestStock.Quantity--;  // Decrease stock by sold quantity
                                bool resultStockUpdate = await _unitOfWork.StockHistoryRepository.SaveOrUpdateAsync(latestStock);
                                if (!resultStockUpdate) return (false, "Failed to update stock quantity.");
                            }    
                        }
                        else
                        {
                            return (false, "Stock record not found.");
                        }


                        //save data billing
                        var billing = new Billing
                        {
                            SaleOrderId = saleOrder.SalesOrderId,
                            UserId = model.UserId,
                            BillingDate = DateTime.Now,
                            Amount = totalAmount,
                            PaymentMethod = model.PaymentMethod,
                            Status = Constant.PAID,
                        };
                        if (string.IsNullOrEmpty(model.Notes))
                        {
                            if (string.IsNullOrEmpty(model.Notes))
                                billing.Notes = Constant.NOTE_PAID;
                            else
                                billing.Notes = model.Notes;
                        }
                        bool resultBilling = await _unitOfWork.BillingRepository.SaveOrUpdateAsync(billing);
                        if (!resultBilling) return (false, "Failed to update billing quantity.");
                    }
                }
                else
                {
                    saleOrder = await _unitOfWork.SalesOrderRepository.GetByIdAsync(model.SalesOrderId);
                    if (saleOrder == null)
                        return (false, "Sale order not found");
                    saleOrder.Status = model.Status;
                    saleOrder.UpdateBy = "Admin";
                    saleOrder.UpdateDate = DateTime.Now;
                    bool result = await _unitOfWork.SalesOrderRepository.SaveOrUpdateAsync(saleOrder);
                    if (!result) return (false, "Failed to update sales order.");

                    if(model.Status != null && model.Status.Contains(Constant.COMPLETED))
                    {
                        // Create StockHistory for the sale (stock out)
                        var stock = new StockHistory
                        {
                            VehicleId = (int)model.VehicleId,
                            ChangeDate = DateTime.Now,
                            Quantity = 1,
                            ChangeType = Constant.STOCKOUT
                        };
                        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");
                        if (userIdClaim != null)
                        {
                            stock.UserId = int.Parse(userIdClaim.Value);
                        }
                        // Lưu StockHistory vào database
                        bool resultStock = await _unitOfWork.StockHistoryRepository.SaveOrUpdateAsync(stock);
                        if (!resultStock) return (false, "Failed to save stock history.");
                    }
                }

                await _unitOfWork.SaveChangeAsync();
                return (true, "Sale order created successfully.");
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public async Task<List<UserDTO>> GetAllUserAsync()
        {
            var userQuery = _unitOfWork.UserRepository.GetAllAsync();
            var users = await userQuery;
            var userList = users.ToList();
            var data = _mapper.Map<List<UserDTO>>(userList);
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

        public async Task<SaleOrderViewModel> GetDataByIdAsync(int id)
        {
            var salesOrderDetails = await _unitOfWork.SalesOrderDetailRepository.GetAllAsync(
                            expression: x => x.SalesOrderId == id,
                            include: query => query
                                .Include(x => x.SalesOrder)
                        );

            // Ánh xạ dữ liệu sang DTO
            var result = salesOrderDetails.Select(detail => new SaleOrderViewModel
            {
               SalesOrderId = detail.SalesOrderId,
               VehicleId = detail.VehicleId,
               Status = detail.SalesOrder.Status,
               UserId = detail.SalesOrder.UserId
            }).FirstOrDefault();

            return result;
        }

        public async Task<SaleOrderExportDTO> GetDataExportByIdAsync(int SalesOrderId)
        {
           /* // Lấy thông tin đơn hàng từ SalesOrderRepository
            var salesOrder = await _unitOfWork.SalesOrderRepository.GetAllAsync(
                expression: x => x.SalesOrderId == SalesOrderId, // Kiểm tra đúng SalesOrderId
                include: query => query.Include(x => x.User)    // Nạp thông tin User liên quan
            );
            // Ném ngoại lệ nếu không tìm thấy đơn hàng
            if (salesOrder == null || !salesOrder.Any())
            {
                throw new KeyNotFoundException($"SalesOrder with ID {SalesOrderId} not found.");
            }*/
           return null;
        }
    }
}
