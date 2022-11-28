using GazpromVehicleBackend.Repositories;
using GazpromVehicleBackend.Shared.Models.Requests;
using GazpromVehicleBackend.Shared.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GazpromVehicleBackend.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehiclesController(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    [HttpGet]
    public async Task<List<VehicleDto>> GetAllVehicles()
    {
        return await _vehicleRepository.GetAllVehicles();
    }

    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult> AddVehicle([FromBody] AddVehicleRequest request)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        var result = await _vehicleRepository.AddVehicle(request);

        if (!result) return BadRequest();

        return Ok();
    }

    [HttpDelete]
    [Route("Remove")]
    public async Task<ActionResult> RemoveVehicle([FromBody] RemoveVehicleRequest request)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _vehicleRepository.RemoveVehicleById(request.VehicleId);
        if (!result) return BadRequest();
        
        return Ok();
    }
}