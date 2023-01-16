using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazpromVehicleBackEnd.DataAccessLayer.Entity
{
    public class VehicleCheck
    {
       
            [Required]
            public  Vehicle Vehicle { get; set; }
            public  User User { get; set; }
            public  ChekList ChekList { get; set; }
        
    }
}
