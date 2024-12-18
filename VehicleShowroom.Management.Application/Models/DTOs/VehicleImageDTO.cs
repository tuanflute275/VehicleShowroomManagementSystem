namespace VehicleShowroom.Management.Application.Models.DTOs
{
    public class VehicleImageDTO
    {
        public int VehicleImageId { get; set; }
        public string Path { get; set; }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
