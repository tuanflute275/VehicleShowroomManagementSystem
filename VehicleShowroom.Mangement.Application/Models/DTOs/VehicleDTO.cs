using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Application.Models.DTOs
{
    public class VehicleDTO
    {
        public int VehicleId { get; set; }
        public string ModelNumber { get; set; }
        public string Name { get; set; }
        public string? Slug { get; set; }
        public string Image { get; set; }
        public string? Status { get; set; }
        public DateTime? DateAdded { get; set; }
        public string? EngineNumber { get; set; }

        public string? ChassisNumber { get; set; }

        public string FuelType { get; set; }

        public string TransmissionType { get; set; }

        public string Color { get; set; }

        public decimal? Price { get; set; }

        public decimal? Mileage { get; set; }

        public int? ManufactureYear { get; set; }
        public string? Description { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
