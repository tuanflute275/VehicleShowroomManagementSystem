namespace VehicleShowroom.Management.Application.Models.DTOs
{
    public class PurchaseOrderDetailDTO
    {
        public int PurchaseOrderDetailId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }    
        public string SupplierPhone { get; set; }
        public string SupplierEmail { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public List<VehiclePurchase> ListVehicle {  get; set; }

    }
    public class VehiclePurchase
    {
        public int VehicleId { get; set; } // Thêm VehicleId
        public string? VehicleName { get; set; } // Tùy chọn: thêm tên xe
        public string ModelNumber { get; set; }
        public string Image { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
