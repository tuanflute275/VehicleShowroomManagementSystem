using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Domain.Abstract;
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

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var sale = await _unitOfWork.SalesOrderRepository.GetByIdAsync(id);
                if (sale == null)
                {
                    return false;
                }

                await _unitOfWork.SalesOrderRepository.DeleteAsync(sale);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(SaleOrderViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
