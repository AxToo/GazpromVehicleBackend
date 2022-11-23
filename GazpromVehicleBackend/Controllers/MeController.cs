using GazpromVehicleBackEnd.DataAccessLayer.Entity;
using GazpromVehicleBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GazpromVehicleBackend.Controllers;

[Authorize]
[Route("api/[controller]")]
public class MeController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public MeController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<User>> Get()
    {
        return await _userRepository.GetUserAsync(HttpContext.User);
    }
}