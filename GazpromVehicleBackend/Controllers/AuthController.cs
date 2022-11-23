using GazpromVehicleBackend.Models;
using GazpromVehicleBackend.Repositories;
using GazpromVehicleBackend.Shared.Models.Requests;
using GazpromVehicleBackend.Shared.Models.Responses;
using GazpromVehicleBackend.TokenGenerators;
using Microsoft.AspNetCore.Mvc;

namespace GazpromVehicleBackend.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AccessTokenGenerator _accessTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthController(AccessTokenGenerator accessTokenGenerator,
        IUserRepository userRepository)
    {
        _accessTokenGenerator = accessTokenGenerator;
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<ActionResult<AuthenticatedUser>> GetToken([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (!await _userRepository.IsValidUsernameAndPassword(request.UserName, request.Password)) return BadRequest();

        var user = await _userRepository.GetUserByEmailAsync(request.UserName);
        var accessToken = _accessTokenGenerator.GenerateToken(user);

        return Ok(new AuthenticatedUser
        {
            AccessToken = accessToken.Value,
            AccessTokenExpirationTime = accessToken.ExpirationTime,
            UserName = user.Email
        });
    }
}