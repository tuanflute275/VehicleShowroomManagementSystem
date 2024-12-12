
using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Application.Models.ViewModels
{
    public class VehicleDetailViewModel
    {
        public int VehicleDetailId { get; set; }

        [Required(ErrorMessage = "EngineNumber is required.")]
        public string EngineNumber { get; set; }

        [Required(ErrorMessage = "ChassisNumber is required.")]
        public string ChassisNumber { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "FuelType is required.")]
        public string? FuelType { get; set; }

        [Required(ErrorMessage = "ManufactureYear is required.")]
        public int? ManufactureYear { get; set; }

        [Required(ErrorMessage = "TransmissionType is required.")]
        public string? TransmissionType { get; set; }

        [Required(ErrorMessage = "Mileage is required.")]
        public int? Mileage { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        public decimal? Weight { get; set; }

        [Required(ErrorMessage = "Dimensions is required.")]
        public string? Dimensions { get; set; }

        [Required(ErrorMessage = "ColorCode is required.")]
        public string? ColorCode { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Vehicle.")]
        public int VehicleId { get; set; }
    }
}
