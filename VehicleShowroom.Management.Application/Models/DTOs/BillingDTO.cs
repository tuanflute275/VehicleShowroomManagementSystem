namespace VehicleShowroom.Management.Application.Models.DTOs
{
    public class BillingDTO
    {
        public int BillingId { get; set; }
        public int? SaleOrderId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PaymentMethod { get; set; }
        public string? BillingDate { get; set; }
        public string? Note { get; set; }
        public decimal? Amount { get; set; }
        public string? Status { get; set; }
    }
}
