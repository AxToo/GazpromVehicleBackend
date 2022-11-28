using GazpromVehicleBackend.Shared.Models.Requests;

namespace GazpromVehicleBackend.Repositories;

public interface IVehicleRepository
{
    Task<bool> AddVehicle(AddVehicleRequest request);
    Task<bool> RemoveVehicleById(int id);
}