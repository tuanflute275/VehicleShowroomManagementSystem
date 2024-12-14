using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Domain.Abstract;
using X.PagedList;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

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
            throw new NotImplementedException();
        }

        public async Task<List<StockHistoryDTO>> GetAllAsync()
        {
            var stockQuery = _unitOfWork.StockHistoryRepository.GetAllAsync(
            null,
              include: query => query
              .Include(x => x.User)
              .Include(x => x.Vehicle)
              .ThenInclude(v => v.Supplier)
            );
            var stocks = await stockQuery;
            var stockList = stocks.ToList();
            foreach (var stock in stockList)
            {
                var company = await _unitOfWork.CompanyRepository.GetByIdAsync(stock.Vehicle.CompanyId);
                stock.Vehicle.Company = company;
            }
            var data = _mapper.Map<List<StockHistoryDTO>>(stockList);
            return data;
        }

        public async Task<IPagedList<StockHistoryDTO>> GetAllPaginationAsync(string? keyword, int page, int pageSize = 8)
        {
            var stockQuery = _unitOfWork.StockHistoryRepository.GetAllAsync(
               expression: s => string.IsNullOrEmpty(keyword) || s.Vehicle.Name.Contains(keyword),
               include: query => query
               .Include(x => x.User)
               .Include(x => x.Vehicle)
               .ThenInclude(v => v.Supplier)
             );
            var stocks = await stockQuery;
            var stockList = stocks.ToList();
            foreach (var stock in stockList)
            {
                var company = await _unitOfWork.CompanyRepository.GetByIdAsync(stock.Vehicle.CompanyId);
                stock.Vehicle.Company = company;
            }


            var data = _mapper.Map<List<StockHistoryDTO>>(stockList);
            return data.ToPagedList(page, pageSize);
        }

        public async Task<StockHistoryDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
