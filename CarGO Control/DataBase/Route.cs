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
        public int Id { get; set; }
        public string CityFrom { get; set; } = null!;
        public string CityTo { get; set; } = null!;

        public string RouteHTTP { get; set; } = null!;
        public DateTime DataOut { get; set; }
        public DateTime DataIn { get; set; }
    }
}
