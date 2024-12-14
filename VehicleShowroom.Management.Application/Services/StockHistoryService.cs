using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Domain.Abstract;
using X.PagedList;

namespace VehicleShowroom.Management.Application.Services
{
    public class StockHistoryService : IStockHistoryService
    {
        IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StockHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var stock = await _unitOfWork.StockHistoryRepository.GetByIdAsync(id);
                if (stock == null)
                {
                    return false;
                }

                await _unitOfWork.StockHistoryRepository.DeleteAsync(stock);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<StockHistoryDTO>> GetAllAsync(string type)
        {
            var stockQuery = _unitOfWork.StockHistoryRepository.GetAllAsync(
               expression: x => string.IsNullOrEmpty(type) || x.ChangeType.Contains(type),
                include: query => query
                .Include(x => x.User)
                .Include(x => x.Vehicle)
                .ThenInclude(v => v.Supplier)
                .Include(x => x.Vehicle.Company)
            );

            var stocks = await stockQuery;
            var stockList = stocks.ToList();
            var data = _mapper.Map<List<StockHistoryDTO>>(stockList);
            return data;
        }

        public async Task<IPagedList<StockHistoryDTO>> GetAllPaginationAsync(string? keyword, int page, int pageSize = 8)
        {
            var stockQuery = _unitOfWork.StockHistoryRepository.GetAllAsync(
               expression: s => string.IsNullOrEmpty(keyword) || s.Vehicle.Name.Contains(keyword) 
               || s.Vehicle.ModelNumber.Contains(keyword) 
               || s.Vehicle.Supplier.SupplierName.Contains(keyword) || s.Vehicle.Company.CompanyName.Contains(keyword),
               include: query => query
               .Include(x => x.User)
               .Include(x => x.Vehicle)
                .ThenInclude(v => v.Supplier)
                .Include(x => x.Vehicle.Company)
             );
            var stocks = await stockQuery;
            var stockList = stocks.ToList();
            var data = _mapper.Map<List<StockHistoryDTO>>(stockList);
            return data.ToPagedList(page, pageSize);
        }
    }
}
