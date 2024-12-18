using Microsoft.AspNetCore.Http;

namespace VehicleShowroom.Management.Application.Models.ViewModels
{
    public class VehicleImageViewModel
    {
        public int VehicleId { get; set; }
        public List<IFormFile>? fileUploads { get; set; }
    }
}
