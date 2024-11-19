using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class Route
    {
        [Key]
        public int ID { get; set; }
        public int? DriverID {  get; set; }
        public int? IDCarGo {  get; set; }
        public int? IDTruck { get; set; }
        public int TrackNumer { get; set; }
        public string CityFrom { get; set; } = null!;
        public string CityTo { get; set; } = null!;
        public string RouteHTTP { get; set; } = null!;
        public DateTime DataOut { get; set; }
        public DateTime DataIn { get; set; }

        public virtual Driver Driver { get; set; }
        public virtual Truck Truck { get; set; }
        public virtual Cargo Cargo { get; set; }

    }
}
