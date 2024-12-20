using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Management.Application.Models.DTOs
{
    public class VehicleDTO
    {
        public int VehicleId { get; set; }
        public string ModelNumber { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string? Status { get; set; }
        public DateTime? DateAdded { get; set; }
        public string? Description { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public string? EngineNumber { get; set; } //Số máy

        public string? ChassisNumber { get; set; } //Số khung

        public string FuelType { get; set; } // Loại nhiên liệu

        public string TransmissionType { get; set; } // Loại hộp số

        public string Color { get; set; } // Màu sắc xe

        public decimal? Price { get; set; } // Giá xe

        public decimal? Mileage { get; set; } // Số km đã đi

        public int? ManufactureYear { get; set; } // Năm sản xuất
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
