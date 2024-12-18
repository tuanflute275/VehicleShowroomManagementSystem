using Microsoft.AspNetCore.Http;

namespace VehicleShowroom.Management.Application.Models.ViewModels
{
    public class VehicleImageEditViewModel
    {
        public int VehicleId { get; set; }
        public int VehicleImageId { get; set; }
        public string Path { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
