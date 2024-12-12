using VehicleShowroom.Management.Application.Models.DTOs;

namespace VehicleShowroom.Management.Application.Abstracts
{
    public interface IHomeService
    {
        Task<CountDTO> GetDashboardCountsAsync();
    }
}
