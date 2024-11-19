using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGO_Control.DataBase
{
    public class Users
    {
        [Key]
        public int ID { get; set; }
        public string Login { get;set ; } = null!;
        public string Password { get; set; } = null!;
        public int? RoleID { get; set; }

        public virtual Roles Roles { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual Operator Operator { get; set; }
    }
}
