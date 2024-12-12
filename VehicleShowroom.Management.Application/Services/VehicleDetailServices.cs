
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Mangement.Application.Abstracts;
using VehicleShowroom.Mangement.Application.Models.DTOs;
using VehicleShowroom.Mangement.Application.Models.ViewModels;
using VehicleShowroom.Mangement.Application.Utils;
using VehicleShowroom.Mangement.Domain.Abstract;
using VehicleShowroom.Mangement.Domain.Entities;
using X.PagedList;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace VehicleShowroom.Mangement.Application.Services
{
    public class VehicleDetailServices : IVehicleDetailServices
    {
        IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private FileUploadHelper _fileUploadHelper;
        public VehicleDetailServices(IUnitOfWork unitOfWork, IMapper mapper, FileUploadHelper fileUploadHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileUploadHelper = fileUploadHelper;
        }

        public async Task<IPagedList<VehicleDetailDTO>> GetAllPaginationAsync(int page, int pageSize = 8)
        {
            var vehicleDetailQuery = _unitOfWork.VehicleDetailRepository.GetAllAsync();
            var vehicleDetail = await vehicleDetailQuery;
            var vehicleDetailList = vehicleDetail.ToList();
            var data = _mapper.Map<List<VehicleDetailDTO>>(vehicleDetailList);
            return data.ToPagedList(page, pageSize);
        }

        public async Task<List<Vehicle>> GetAllVehicleAsync()
        {
            var vehicleQuery = _unitOfWork.VehicleRepository.GetAllAsync();
            var vehicles = await vehicleQuery;
            var vehicleList = vehicles.ToList();
            return vehicleList;
        }

        public async Task<VehicleDetail> GetByIdAsync(int id)
        {
            var vehicleDetail = await _unitOfWork.VehicleDetailRepository.GetAllAsync(
            expression: v => v.VehicleDetailId == id,
            include: query => query
                .Include(x => x.Vehicle)
           );

            return vehicleDetail.FirstOrDefault();
        }

        public async Task<bool> SaveOrUpdateAsync(VehicleDetailViewModel model)
        {
            try
            {
                var vehicleDetail = new VehicleDetail();
                if (model.VehicleDetailId == 0)
                {
                    vehicleDetail.EngineNumber = model.EngineNumber;
                    vehicleDetail.ChassisNumber = model.ChassisNumber;
                    vehicleDetail.Color = model.Color;
                    vehicleDetail.Price = model.Price;
                    vehicleDetail.FuelType = model.FuelType;
                    vehicleDetail.ManufactureYear = model.ManufactureYear;
                    vehicleDetail.TransmissionType = model.TransmissionType;
                    vehicleDetail.Mileage = model.Mileage;
                    vehicleDetail.Weight = model.Weight;
                    vehicleDetail.Dimensions = model.Dimensions;
                    vehicleDetail.VehicleId = model.VehicleId;
                    vehicleDetail.ColorCode = model.ColorCode;
                    vehicleDetail.CreateBy = "Admin";
                    vehicleDetail.CreateDate = DateTime.Now;
                }
                else
                {
                    vehicleDetail = await _unitOfWork.VehicleDetailRepository.GetByIdAsync(model.VehicleDetailId);
                    vehicleDetail.EngineNumber = model.EngineNumber;
                    vehicleDetail.ChassisNumber = model.ChassisNumber;
                    vehicleDetail.Color = model.Color;
                    vehicleDetail.Price = model.Price;
                    vehicleDetail.FuelType = model.FuelType;
                    vehicleDetail.ManufactureYear = model.ManufactureYear;
                    vehicleDetail.TransmissionType = model.TransmissionType;
                    vehicleDetail.Mileage = model.Mileage;
                    vehicleDetail.Weight = model.Weight;
                    vehicleDetail.Dimensions = model.Dimensions;
                    vehicleDetail.VehicleId = model.VehicleId;
                    vehicleDetail.ColorCode = model.ColorCode;
                    vehicleDetail.CreateBy = "Admin";
                    vehicleDetail.CreateDate = DateTime.Now;
                }
                bool result = await _unitOfWork.VehicleDetailRepository.SaveOrUpdateAsync(vehicleDetail);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var vehicleDetail = await _unitOfWork.VehicleDetailRepository.GetByIdAsync(id);
                if (vehicleDetail == null)
                {
                    return false;
                }

                await _unitOfWork.VehicleDetailRepository.DeleteAsync(vehicleDetail);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
