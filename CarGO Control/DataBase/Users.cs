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
        public int UserID { get; set; }
        public string Login { get;set ; } = null!;
        public string Password { get; set; } = null!;

        // Внешний ключ
        public int RoleID { get; set; }

        // Навигационное свойство для связи с Roles
        public virtual Roles Role { get; set; } = null!;
    }
}
