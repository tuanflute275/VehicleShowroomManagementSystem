namespace VehicleShowroom.Management.Application.Models.DTOs
{
    public class SalesOrderDetailDTO
    {
        public string Image { get; set; }
        public string VehicleName { get; set; }
        public string ModelNumber { get; set; }
        public string SupplierName { get; set; }
        public string CompanyName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
