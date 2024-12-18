namespace VehicleShowroom.Management.Application.Models.ViewModels
{
    public class SaleOrderUpdateViewModel
    {
        public int SalesOrderId { get; set; }
        public string? Status { get; set; }
        public int? VehicleId { get; set; }
    }
}
