using GazpromVehicleBackend.Shared.Models.Requests;
using GazpromVehicleBackend.Shared.Models.Responses;

namespace GazpromVehicleBackend.Repositories;

public interface IVehicleRepository
{
    Task<List<VehicleDto>> GetAllVehicles();
    Task<bool> AddVehicle(AddVehicleRequest request);
    Task<bool> RemoveVehicleById(int id);
    Task<bool> RemoveVehicleByRegNumber(string regNumber);
}