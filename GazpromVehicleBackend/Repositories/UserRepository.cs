using System.Security.Claims;
using GazpromVehicleBackEnd.DataAccessLayer;
using GazpromVehicleBackEnd.DataAccessLayer.Entity;
using GazpromVehicleBackend.Shared.Models.Responses;
using Microsoft.AspNetCore.Identity;

namespace GazpromVehicleBackend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRepository(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
     public async Task<User> GetUserAsync(ClaimsPrincipal principal)
    {
        return await _userManager.GetUserAsync(principal);
    }

    public async Task<User> GetUserById(string id) => await _userManager.FindByIdAsync(id);

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> IsValidUsernameAndPassword(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<bool> AddUserAsync(string emailAddress, string password)
    {
        var existingUser = await _userManager.FindByEmailAsync(emailAddress);

        if (existingUser != null) return false;

        User newUser = new()
        {
            Email = emailAddress,
            EmailConfirmed = true,
            UserName = emailAddress
        };

        var createdUserResult = await _userManager.CreateAsync(newUser, password);
        var user = await _userManager.FindByEmailAsync(emailAddress);
        var addUserToRole = await _userManager.AddToRoleAsync(user, "User");

        return createdUserResult.Succeeded && addUserToRole.Succeeded;
    }

    public async Task<bool> RemoveUserAsync(string emailAddress)
    {
        var user = await _userManager.FindByEmailAsync(emailAddress);

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    public Dictionary<string, string> GetAllRoles() => _context.Roles.ToDictionary(x => x.Id, x => x.Name);

    public async Task<bool> AddRole(string role)
    {
        var result = await _roleManager.CreateAsync(new IdentityRole(role));
        return result.Succeeded;
    }

    public async Task<bool> UpdateRole(string roleId, string roleName)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        role.Name = roleName;

        var result = await _roleManager.UpdateAsync(role);
        return result.Succeeded;
    }

    public async Task<bool> RemoveRole(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        var result = await _roleManager.DeleteAsync(role);

        return result.Succeeded;
    }

    public async Task AddUserToRole(string emailAddress, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(emailAddress);

        await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task RemoveUserFromRole(string emailAddress, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(emailAddress);

        await _userManager.RemoveFromRoleAsync(user, roleName);
        
        await _context.SaveChangesAsync();
    }

    public List<ApplicationUser> GetAllUsers()
    {
        var users = _context.Users.ToList();

        var userRoles = _context.UserRoles
            .Join(_context.Roles, ur => ur.RoleId, r => r.Id,
                (ur, r) => new { ur.UserId, ur.RoleId, r.Name });

        return users.Select(user => new ApplicationUser
        {
            Id = user.Id,
            Email = user.Email,
            Roles = userRoles
                .Where(x => x.UserId == user.Id)
                .ToDictionary(x => x.RoleId, x => x.Name)
        }).ToList();
    }
}