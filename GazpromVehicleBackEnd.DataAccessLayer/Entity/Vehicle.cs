﻿using System.ComponentModel.DataAnnotations;

namespace GazpromVehicleBackEnd.DataAccessLayer.Entity;

public class Vehicle
{
    [Required]
    public int Id { get; set; }
    public string Name { get; set; }
    public string RegistrationNumber { get; set; }
    public bool IsChecked { get; set; }
}