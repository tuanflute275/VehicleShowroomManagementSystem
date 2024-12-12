using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Management.Application.Models.ViewModels
{
    public class PurchaseOrderViewModel
    {
        public int PurchaseOrderId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Supplier.")]
        public int SupplierId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Vehicle.")]
        public int VehicleId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }
    }
}
