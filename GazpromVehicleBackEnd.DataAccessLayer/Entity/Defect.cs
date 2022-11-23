using System.ComponentModel.DataAnnotations;

namespace GazpromVehicleBackEnd.DataAccessLayer.Entity;

public class Defect
{
    [Required]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Sanction Sanction { get; set; }
}