using System.Security.Claims;
using GazpromVehicleBackEnd.DataAccessLayer.Entity;
using GazpromVehicleBackend.Shared.Models.Responses;

namespace GazpromVehicleBackend.Repositories;

public interface IUserRepository
{
    Task<User> GetUserAsync(ClaimsPrincipal principal);
    Task<User> GetUserById(string id);
    Task<User> GetUserByEmailAsync(string email);
    Task<bool> IsValidUsernameAndPassword(string username, string password);
    Task<bool> AddUserAsync(string emailAddress, string password);
    Task<bool> RemoveUserAsync(string emailAddress);
    Dictionary<string, string> GetAllRoles();
    Task<bool> AddRole(string role);
    Task<bool> UpdateRole(string roleId, string roleName);
    Task<bool> RemoveRole(string roleId);
    Task AddUserToRole(string emailAddress, string roleName);
    Task RemoveUserFromRole(string emailAddress, string roleName);
    List<ApplicationUser> GetAllUsers();
}