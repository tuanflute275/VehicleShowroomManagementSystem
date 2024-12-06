using AutoMapper;
using VehicleShowroom.Mangement.Application.Models.DTOs.Supplier;
using VehicleShowroom.Mangement.Application.Models.ViewModels;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.Application.Configuration
{
    public class AutomapConfig : Profile
    {
        public AutomapConfig()
        {
            CreateMap<Supplier, SupplierViewModel>().ReverseMap();
            CreateMap<Supplier, SupplierDTO>().ReverseMap();
        }
    }
}
