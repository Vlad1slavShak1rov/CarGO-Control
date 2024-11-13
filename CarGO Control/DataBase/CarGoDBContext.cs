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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Driver)
                .WithOne(up => up.User)
                .HasForeignKey<Driver>(up => up.UserID);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Operator)
                .WithOne(up => up.User)
                .HasForeignKey<Operator>(up => up.UserID);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Roles)
                .WithOne(up => up.Users)
                .HasForeignKey<Roles>(up => up.RoleID);

            modelBuilder.Entity<Transportation>()
                .HasOne(u => u.Driver)
                .WithOne(up => up.Transportation)
                .HasForeignKey<Driver>(up => up.IDTransportation);

            modelBuilder.Entity<Transportation>()
                .HasOne(u => u.Route)
                .WithOne(up => up.Transport)
                .HasForeignKey<Route>(up => up.TrackNumer);
        }

    }
}
