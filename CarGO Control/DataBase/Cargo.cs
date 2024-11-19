using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class Cargo
    {
        [Key]
        public int ID { get; set; }
        public string CargoType { get; set; } = null!;
        public string Contents { get; set; } = null!;
        public int Weight { get; set; }

        public virtual Route Route { get; set; }
    }
}
