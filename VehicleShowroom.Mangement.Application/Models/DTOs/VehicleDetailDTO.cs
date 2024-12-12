
namespace VehicleShowroom.Mangement.Application.Models.DTOs
{
    public class VehicleDetailDTO
    {
        public int VehicleDetailId { get; set; }
        public int VehicleId { get; set; }
        public string Name { get; set; }
        public string EngineNumber { get; set; }
        public string ChassisNumber { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string? FuelType { get; set; }
        public int? ManufactureYear { get; set; }
        public string? TransmissionType { get; set; }
        public int? Mileage { get; set; }
        public decimal? Weight { get; set; }
        public string? Dimensions { get; set; }
        public string? ColorCode { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
