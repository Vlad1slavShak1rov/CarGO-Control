using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class Roles
    {
        [Key]
        public int? RoleID { get; set; }
        public string RoleName { get; set; } = null!;

        public Users Users { get; set; }
    }
}
