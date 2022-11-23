using System.Security.Claims;
using GazpromVehicleBackEnd.DataAccessLayer;
using GazpromVehicleBackEnd.DataAccessLayer.Entity;
using GazpromVehicleBackend.Models;

namespace GazpromVehicleBackend.TokenGenerators;

public class AccessTokenGenerator
{
    private readonly ApplicationDbContext _context;
    private readonly TokenGenerator _tokenGenerator;
    private readonly AuthConfiguration _configuration;
    
    public AccessTokenGenerator(TokenGenerator tokenGenerator, AuthConfiguration configuration, ApplicationDbContext context)
    {
        _tokenGenerator = tokenGenerator;
        _configuration = configuration;
        _context = context;
    }
    
    public AccessToken GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName),
        };
        
        var roles = _context.UserRoles
            .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
            .Where(t => t.ur.UserId == user.Id)
            .Select(t => new Claim(ClaimTypes.Role, t.r.Name));
        
        claims.AddRange(roles);
        

        var expirationTime = DateTime.UtcNow.AddMinutes(_configuration.AccessTokenExpirationMinutes);
        var value = _tokenGenerator.GenerateToken(
            _configuration.AccessTokenSecret,
            _configuration.Issuer,
            _configuration.Audience,
            expirationTime,
            claims);

        return new AccessToken()
        {
            Value = value,
            ExpirationTime = expirationTime
        };
    }
}