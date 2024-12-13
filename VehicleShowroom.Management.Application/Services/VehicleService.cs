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
    public class VehicleService : IVehicleService
    {
        IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private FileUploadHelper _fileUploadHelper;
        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper, FileUploadHelper fileUploadHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileUploadHelper = fileUploadHelper;
        }

        public async Task<IPagedList<VehicleDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8)
        {
            var vehicleQuery = _unitOfWork.VehicleRepository.GetAllAsync(
               expression: s => string.IsNullOrEmpty(keyword) || s.Name.Contains(keyword),
               include: query => query
               .Include(x => x.Supplier)   
               .Include(x => x.Company)       
             );
            var vehicle = await vehicleQuery;
            var vehicleList = vehicle.ToList();
            var data = _mapper.Map<List<VehicleDTO>>(vehicleList);
            return data.ToPagedList(page, pageSize);
        }

        public async Task<List<Supplier>> GetAllSupplierAsync()
        {
            var supplierQuery = _unitOfWork.SupplierRepository.GetAllAsync();
            var suppliers = await supplierQuery;
            var supplierList = suppliers.ToList();
            return supplierList;
        }
        public async Task<List<Company>> GetAllCompanyAsync()
        {
            var companyQuery = _unitOfWork.CompanyRepository.GetAllAsync();
            var companies = await companyQuery;
            var companyList = companies.ToList();
            return companyList;
        }

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetAllAsync(
             expression: v => v.VehicleId == id,
             include: query => query
                 .Include(x => x.Supplier)
                 .Include(x => x.Company)
            );

            return vehicle.FirstOrDefault();
        }

        public async Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(VehicleViewModel model, IFormFile? fileUpload, string? oldImage = null)
        {
            try
            {
                var vehicle = new Vehicle();
                if (model.VehicleId == 0)
                {
                    vehicle.ModelNumber = model.ModelNumber;
                    vehicle.Name = model.Name;
                    vehicle.Slug = Util.GenerateSlug(model.Name);
                    vehicle.EngineNumber = model.EngineNumber;
                    vehicle.ChassisNumber = model.ChassisNumber;
                    vehicle.FuelType = model.FuelType;
                    vehicle.TransmissionType = model.TransmissionType;
                    vehicle.Color = model.Color;
                    vehicle.Price = model.Price;
                    vehicle.Mileage = model.Mileage;
                    vehicle.ManufactureYear = model.ManufactureYear;
                    vehicle.Status = model.Status;
                    vehicle.Description = model.Description;
                    vehicle.SupplierId = model.SupplierId;
                    vehicle.CompanyId = model.CompanyId;
                    vehicle.CreateBy = "Admin";
                    vehicle.CreateDate = DateTime.Now;
                    if (fileUpload != null)
                    {
                        var image = await _fileUploadHelper.UploadFileAsync(fileUpload, "vehicles");
                        vehicle.Image = image;
                    }
                }
                else
                {
                    vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(model.VehicleId);
                    if (vehicle == null)
                        return (false, "User not found");
                    vehicle.ModelNumber = model.ModelNumber;
                    vehicle.Name = model.Name;
                    vehicle.Slug = Util.GenerateSlug(model.Name);
                    vehicle.EngineNumber = model.EngineNumber;
                    vehicle.ChassisNumber = model.ChassisNumber;
                    vehicle.FuelType = model.FuelType;
                    vehicle.TransmissionType = model.TransmissionType;
                    vehicle.Color = model.Color;
                    vehicle.Price = model.Price;
                    vehicle.Mileage = model.Mileage;
                    vehicle.ManufactureYear = model.ManufactureYear;
                    vehicle.Status = model.Status;
                    vehicle.Description = model.Description;
                    vehicle.SupplierId = model.SupplierId;
                    vehicle.CompanyId = model.CompanyId;
                    vehicle.UpdateBy = "Admin";
                    vehicle.UpdateDate = DateTime.Now;
                    if (fileUpload != null) // Kiểm tra xem ImageFile có tồn tại hay không
                    {
                        var image = await _fileUploadHelper.UploadFileAsync(fileUpload, "vehicles");
                        vehicle.Image = image;
                    }
                }
                bool result = await _unitOfWork.VehicleRepository.SaveOrUpdateAsync(vehicle);
                await _unitOfWork.SaveChangeAsync();
                return (true, null);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);
                if (vehicle == null)
                {
                    return false;
                }

                await _unitOfWork.VehicleRepository.DeleteAsync(vehicle);
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
