using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Domain.Abstract;
using X.PagedList;

namespace VehicleShowroom.Management.Application.Services
{
    public class BillingService : IBillingService
    {
        IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BillingService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            try
            {
                var billing = await _unitOfWork.BillingRepository.GetByIdAsync(id);
                if (billing == null) return (false, "Billing record not found.");

                await _unitOfWork.BillingRepository.DeleteAsync(billing);
                await _unitOfWork.SaveChangeAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<IPagedList<BillingDTO>> GetAllPaginationAsync(string? keyword, int page, int pageSize = 8)
        {
            var query = _unitOfWork.BillingRepository.GetAllAsync(
               expression: x => string.IsNullOrEmpty(keyword) || x.User.FullName.Contains(keyword)
               || x.User.PhoneNumber.Contains(keyword),
                include: query => query
                .Include(x => x.User)
                .Include(x => x.SalesOrder)
            );
            var billings = await query;
            var billingList = billings.ToList();
            var data = _mapper.Map<List<BillingDTO>>(billingList);
            return data.ToPagedList(page, pageSize);
        }
    }
}
