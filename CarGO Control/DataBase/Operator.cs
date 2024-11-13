using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class Operator
    {
        [Key]
        public int ID { get; set; }  
        public string Name { get; set; } = null!;
        public int UserID { get; set; }
        public virtual Users User { get; set; }
    }
}
