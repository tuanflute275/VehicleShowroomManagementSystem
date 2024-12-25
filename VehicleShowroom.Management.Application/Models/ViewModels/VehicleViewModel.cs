using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Management.Application.Models.ViewModels
{
    public class VehicleViewModel
    {
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Model Number is required.")]
        [StringLength(50, ErrorMessage = "Model Number cannot exceed 50 characters.")]
        public string ModelNumber { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }
        public string? Image { get; set; }

        [RegularExpression("^(Available|In Service|Sold)$", ErrorMessage = "Status must be either 'Available' or 'In Service' or 'Sold'.")]
        public string? Status { get; set; } = "Active";

        public string? EngineNumber { get; set; } = "";
        public string? ChassisNumber { get; set; } = "";
        public string? FuelType { get; set; } = "";
        public string? TransmissionType { get; set; } = "";
        public string? Color { get; set; } = "";

        [Range(1, int.MaxValue, ErrorMessage = "Price is required.")]
        public decimal? Price { get; set; }
        public decimal? Mileage { get; set; }
        public int? ManufactureYear { get; set; }

        public string? Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Supplier.")]
        public int SupplierId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Company.")]
        public int CompanyId { get; set; }
    }

}
