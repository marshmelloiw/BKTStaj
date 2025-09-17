using KampusTek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace KampusTek.Data
{
    public class KampusTekDbContext : DbContext
    {
        public KampusTekDbContext(DbContextOptions<KampusTekDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Bicycle> Bicycles { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.StartStation)
                .WithMany(s => s.RentalsAsStart)
                .HasForeignKey(r => r.StartStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.EndStation)
                .WithMany(s => s.RentalsAsEnd)
                .HasForeignKey(r => r.EndStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Bicycle)
                .WithMany(b => b.Rentals)
                .HasForeignKey(r => r.BicycleId);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.User)
                .WithMany(u => u.Rentals)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Maintenance>()
                .HasOne(m => m.Bicycle)
                .WithMany(b => b.Maintenances)
                .HasForeignKey(m => m.BicycleId);

            modelBuilder.Entity<Bicycle>()
                .HasOne(b => b.CurrentStation)
                .WithMany(s => s.Bicycles)
                .HasForeignKey(b => b.CurrentStationId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserType)
                .WithMany(ut => ut.Users)
                .HasForeignKey(u => u.UserTypeId);

            modelBuilder.Entity<Bicycle>()
                .HasIndex(b => b.BicycleCode)
                .IsUnique();

            modelBuilder.Entity<UserType>().HasData(
                new UserType { Id = 1, Name = "Student" },
                new UserType { Id = 2, Name = "Staff" }
            );
        }
    }
}
