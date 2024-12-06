using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class Truck
    {
        [Key]
        public int ID { get; set; }
        public string LicensePlate { get; set; }
        public string CarMake { get; set; }

        public virtual Route Route { get; set; }
        public virtual Driver Driver { get; set; }
    }

}
