namespace GazpromVehicleBackend.Shared.Models.Responses;

public class AuthenticatedUser
{
    public string UserName { get; set; }
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpirationTime { get; set; }
}