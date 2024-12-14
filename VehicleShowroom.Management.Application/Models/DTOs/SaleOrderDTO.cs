namespace VehicleShowroom.Management.Application.Models.DTOs
{
    public class SaleOrderDTO
    {
        public int SalesOrderId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Gender { get; set; }
        public string OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
