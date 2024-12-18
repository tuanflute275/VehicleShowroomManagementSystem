using System.ComponentModel.DataAnnotations;
using VehicleShowroom.Management.Application.Models.DTOs;

namespace VehicleShowroom.Management.Application.Models.ViewModels
{
    public class PurchaseOrderViewModel
    {
        public int PurchaseOrderId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Supplier.")]
        public int SupplierId { get; set; }

        [Required]
        public List<PurchaseOrderDetailViewModel> Details { get; set; } = new List<PurchaseOrderDetailViewModel>();
    }
    public class PurchaseOrderDetailViewModel
    {
        public int VehicleId { get; set; }
        public int Quantity { get; set; }
    }
}
