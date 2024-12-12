namespace VehicleShowroom.Management.Application.Models.DTOs
{
    public class PurchaseOrderDTO
    {
        public int PurchaseOrderId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierEmail { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
