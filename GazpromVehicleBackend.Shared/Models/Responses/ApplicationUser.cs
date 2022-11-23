namespace GazpromVehicleBackend.Shared.Models.Responses;

public class ApplicationUser
{
    public string Id { get; set; }
    public string Email { get; set; }
    public Dictionary<string, string> Roles { get; set; } = new();
}