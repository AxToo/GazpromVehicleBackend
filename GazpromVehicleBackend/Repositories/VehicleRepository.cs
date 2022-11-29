using GazpromVehicleBackEnd.DataAccessLayer;
using GazpromVehicleBackEnd.DataAccessLayer.Entity;
using GazpromVehicleBackend.Shared.Models.Requests;
using GazpromVehicleBackend.Shared.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace GazpromVehicleBackend.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _context;

    public VehicleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<VehicleDto>> GetAllVehicles() => await _context.Vehicles.Select(v => new VehicleDto
    {
        Brand = v.Brand,
        RegistrationNumber = v.RegistrationNumber,
        IsChecked = v.IsChecked
    }).ToListAsync();

    public async Task<bool> AddVehicle(AddVehicleRequest request)
    {
        if (string.IsNullOrEmpty(request.Brand) || string.IsNullOrEmpty(request.RegistrationNumber)) return false;

        var vehicle = new Vehicle
        {
            Brand = request.Brand,
            RegistrationNumber = request.RegistrationNumber
        };

        await _context.Vehicles.AddAsync(vehicle);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveVehicleById(int id)
    {
        var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
        if (vehicle == null) return false;

        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveVehicleByRegNumber(string regNumber)
    {
        var vehicle = await _context.Vehicles.AsNoTracking()
            .FirstOrDefaultAsync(v => v.RegistrationNumber == regNumber);
        if (vehicle == null) return false;

        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();

        return true;
    }

}