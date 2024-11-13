using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class Transportation
    {
        [Key]
        public int ID { get; set; }

        public int TrackNumber { get; set; }

        public virtual Route Route { get; set; }
        public virtual Driver Driver { get; set; }
    }

}
