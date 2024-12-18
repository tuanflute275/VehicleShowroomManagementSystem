using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Management.Application.Models.ViewModels
{
    public class SaleOrderViewModel
    {
        public int SalesOrderId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid User.")]
        public int UserId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Vehicle.")]
        public int? VehicleId { get; set; }
        public string? Status { get; set; }
        public string? PaymentMethod { get; set; }
        public string? StatusPayment { get; set; }
        public string? Notes { get; set; }
    }
}
