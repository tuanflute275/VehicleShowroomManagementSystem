namespace VehicleShowroom.Management.Application.Models.DTOs
{
    public class StockHistoryDTO
    {
        public int StockHistoryId { get; set; }
        public string ChangedBy { get; set; }
        public string PhoneNumber { get; set; }
        public string ModelNumber { get; set; }
        public string VehicleName { get; set; }
        public decimal VehiclePrice { get; set; }
        public string SupplierName { get; set; }
        public string CompanyName { get; set; }
        public int Quantity { get; set; }
        public string ChangeType { get; set; }
        public string ChangeDate { get; set; }
    }
}
