using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarGO_Control.DataBase
{
    public class CarGoDBContext:DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Transportation> Transportations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\sakir\\source\\repos\\CarGO Control\\CarGO Control\\DataBase\\DataBase.db");
        }

        
    }
}
