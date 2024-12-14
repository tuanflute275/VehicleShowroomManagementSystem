namespace VehicleShowroom.Management.Application.Models.DTOs
{
    public class UserVehicleInfoDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public List<VehicleUserDTO> Vehicles { get; set; }
    }
    public class VehicleUserDTO
    {
        public string ModelNumber { get; set; }
        public string VehicleName { get; set; }
        public string SupplierName { get; set; }
        public string CompanyName { get; set; }
        public decimal Price { get; set; }
    }
}
