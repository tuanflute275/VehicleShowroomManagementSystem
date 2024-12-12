using AutoMapper;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Domain.Entities;

namespace VehicleShowroom.Management.Application.Configuration
{
    public class AutomapConfig : Profile
    {
        public AutomapConfig()
        {
            CreateMap<Supplier, SupplierViewModel>().ReverseMap();
            CreateMap<Supplier, SupplierDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<UserDTO, UserViewModel>().ReverseMap();
            CreateMap<UserDTO, UserViewModel>().ReverseMap();
            CreateMap<Vehicle, VehicleViewModel>().ReverseMap();
            CreateMap<Vehicle, VehicleDTO>()
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.SupplierName))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName));

            CreateMap<PurchaseOrder, PurchaseOrderDTO>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.SupplierName))
                .ForMember(dest => dest.SupplierPhone, opt => opt.MapFrom(src => src.Supplier.PhoneNumber))
           .ForMember(dest => dest.SupplierEmail, opt => opt.MapFrom(src => src.Supplier.Email));
        }
    }
}
