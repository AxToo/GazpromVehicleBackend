using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazpromVehicleBackEnd.DataAccessLayer.Entity
{
    public class ChekList
    {
        public ChekList(string name)
        {
        }

        [Required]
        public int Id { get; set; }
        public int Number { get; set; }
        public string Chek { get; set; }
        public List<Defect> Defect { get; set; }
        public string Data { get; set; }
    }
}
