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
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Route> Routes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\sakir\\source\\repos\\CarGO Control\\CarGO Control\\DataBase\\DataBase.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>()
                 .HasOne(u => u.Users)
                 .WithOne(up => up.Driver)
                 .HasForeignKey<Driver>(up => up.UserID);

            modelBuilder.Entity<Route>()
                .HasOne(u => u.Driver)
                .WithOne(up => up.Routes)
                .HasForeignKey<Route>(up => up.DriverID);

            modelBuilder.Entity<Truck>()
                .HasOne(u => u.Driver)
                .WithOne(up => up.Trucks)
                .HasForeignKey<Truck>(up => up.DriverID);

            modelBuilder.Entity<Route>()
                .HasOne(u => u.Truck)
                .WithOne(up => up.Route)
                .HasForeignKey<Route>(up => up.IDTruck);

            modelBuilder.Entity<Route>()
                .HasOne(u => u.Cargo)
                .WithOne(up => up.Route)
                .HasForeignKey<Route>(up => up.IDCarGo);

            modelBuilder.Entity<Operator>()
                .HasOne(u => u.User)
                .WithOne(up => up.Operator)
                .HasForeignKey<Operator>(up => up.UserID);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Roles)
                .WithMany(r => r.Users) 
                .HasForeignKey(u => u.RoleID); 
        }

    }
}
