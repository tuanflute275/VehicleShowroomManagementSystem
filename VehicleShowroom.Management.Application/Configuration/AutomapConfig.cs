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
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<Company, CompanyViewModel>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<UserDTO, UserViewModel>().ReverseMap();
            CreateMap<UserDTO, UserViewModel>().ReverseMap();
            CreateMap<Vehicle, VehicleViewModel>().ReverseMap();
            CreateMap<Vehicle, VehicleDTO>()
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.SupplierName))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName));

            CreateMap<VehicleImage, VehicleImageViewModel>().ReverseMap();
            CreateMap<VehicleImage, VehicleImageEditViewModel>().ReverseMap();
            CreateMap<VehicleImage, VehicleImageDTO>()
                .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.Vehicle.VehicleId))
                .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.Vehicle.Name));

            CreateMap<PurchaseOrder, PurchaseOrderDTO>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.SupplierName))
                .ForMember(dest => dest.SupplierPhone, opt => opt.MapFrom(src => src.Supplier.PhoneNumber))
           .ForMember(dest => dest.SupplierEmail, opt => opt.MapFrom(src => src.Supplier.Email));

            CreateMap<StockHistory, StockHistoryDTO>()
             .ForMember(dest => dest.ChangedBy, opt => opt.MapFrom(src => src.User.FullName))
             .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
             .ForMember(dest => dest.ModelNumber, opt => opt.MapFrom(src => src.Vehicle.ModelNumber))
             .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.Vehicle.Name))
             .ForMember(dest => dest.VehiclePrice, opt => opt.MapFrom(src => src.Vehicle.Price != null ? Convert.ToDecimal(src.Vehicle.Price) : 0))  // Cải tiến chuyển đổi an toàn
             .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Vehicle.Supplier.SupplierName))
             .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Vehicle.Company != null ? src.Vehicle.Company.CompanyName : string.Empty));

            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailDTO>()
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.PurchaseOrder.Supplier.SupplierName))
            .ForMember(dest => dest.SupplierPhone, opt => opt.MapFrom(src => src.PurchaseOrder.Supplier.PhoneNumber))
            .ForMember(dest => dest.SupplierEmail, opt => opt.MapFrom(src => src.PurchaseOrder.Supplier.Email))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.PurchaseOrder.OrderDate))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.PurchaseOrder.TotalAmount))
            .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.Vehicle.Name))
            .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.Vehicle.VehicleId));


            CreateMap<SalesOrder, SaleOrderDTO>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.Adress, opt => opt.MapFrom(src => src.User.Address))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));


            CreateMap<SalesOrderDetail, SalesOrderDetailDTO>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Vehicle.Image))
            .ForMember(dest => dest.ModelNumber, opt => opt.MapFrom(src => src.Vehicle.ModelNumber))
            .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.Vehicle.Name))
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Vehicle.Supplier.SupplierName))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Vehicle.Company.CompanyName));
            CreateMap<Billing, BillingDTO>()
               .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.User.FullName))
               .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.User.PhoneNumber))
               .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Notes));
        }
    }
}
