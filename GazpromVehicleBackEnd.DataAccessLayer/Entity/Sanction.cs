using System.ComponentModel.DataAnnotations;

namespace GazpromVehicleBackEnd.DataAccessLayer.Entity;

public class Sanction
{
    [Required]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Fine { get; set; }
    public Defect Defect { get; set; }
}