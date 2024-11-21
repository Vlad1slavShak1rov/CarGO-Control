using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; } = null!;
        public int Experience { get; set; }

        public virtual Users Users { get; set; }
        public virtual Route Routes { get; set; }
        public virtual Truck Trucks { get; set; }

    }
}
