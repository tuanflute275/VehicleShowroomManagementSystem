using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Domain.Abstract;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace VehicleShowroom.Management.Application.Services
{
    public class HomeService : IHomeService
    {
        IUnitOfWork _unitOfWork;
        public HomeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public async Task<CountDTO> GetDashboardCountsAsync()
        {
            var countDto = new CountDTO
            {
                TotalUserCount = await _unitOfWork.UserRepository.CountAsync(),
                TotalSupplierCount = await _unitOfWork.SupplierRepository.CountAsync(),
                TotalCompanyCount = await _unitOfWork.CompanyRepository.CountAsync(),
                TotalVehicleCount = await _unitOfWork.VehicleRepository.CountAsync(),
                TotalPurchaseOrderCount = await _unitOfWork.PurchaseOrderRepository.CountAsync(),
                TotalSalesOrderCount = await _unitOfWork.SalesOrderRepository.CountAsync(),
                TotalStockCount = await _unitOfWork.StockHistoryRepository.CountAsync()
            };

            return countDto;
        }
    }
}
