using GazpromVehicleBackend.Repositories;
using GazpromVehicleBackend.Shared.Models.Requests;
using GazpromVehicleBackend.Shared.Models.Responses;
using GazpromVehicleBackEnd.DataAccessLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GazpromVehicleBackend.Controllers;

[Authorize]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public UserController(IUserRepository userRepository)
    {
        this.userRepository=userRepository;
    }

    [HttpGet]
    public async Task<ApplicationUser> Get()
    {
        var currentUser = await userRepository.GetUserAsync(HttpContext.User);

        return new ApplicationUser
        {
            Id = currentUser.Id,
            Email = currentUser.Email
        };
    }

    [HttpGet]
    [Route("AllUsers")]
    public List<ApplicationUser> GetAll()
    {
        return userRepository.GetAllUsers();
    }

    [HttpPost]
    [Route("Add")]

    public async Task<ActionResult> Add([FromBody] AddUserRequest request)
    {
        var result = await userRepository.AddUserAsync(request.UserName, request.Password, request.Role);
        return result ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("Remove")]

    public async Task<ActionResult> Remove([FromBody] DeleteUserRequest request)

    {
        var result = await userRepository.RemoveUserAsync(request.UserName);
        return result ? Ok() : BadRequest();
    }

    [HttpGet]
    [Route("Roles")]
    
    public Dictionary<string, string> GetAllRoles()
    {
        return userRepository.GetAllRoles();
    }

    [HttpPost]
    [Route("AddRole")]

   public async Task<ActionResult> AddRole([FromBody]AddRoleRequest request)
    {
        var result = await userRepository.AddRole(request.RoleName);
        return result ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("RemoveRole")]

    public async Task<ActionResult> RemoveRole([FromBody] AddRoleRequest request)
    {
        var result = await userRepository.RemoveRole(request.RoleName);
        return result ? Ok() : BadRequest();
    }
}
