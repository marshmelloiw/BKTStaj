using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KampusTek.Models;

public partial class KampusTekDbv2Context : DbContext
{
    public KampusTekDbv2Context()
    {
    }

    public KampusTekDbv2Context(DbContextOptions<KampusTekDbv2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Bicycle> Bicycles { get; set; }

    public virtual DbSet<Maintenance> Maintenances { get; set; }

    public virtual DbSet<RentingProcess> RentingProcesses { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=KampusTekDBv2;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bicycle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bicycles__3213E83F49801FEA");

            entity.HasIndex(e => e.BicycleCode, "UQ__Bicycles__CAEB9D50C5A93013").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BicycleCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("bicycle_code");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.CurrentStationId).HasColumnName("current_station_id");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("model");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.CurrentStation).WithMany(p => p.Bicycles)
                .HasForeignKey(d => d.CurrentStationId)
                .HasConstraintName("station_key");
        });

        modelBuilder.Entity<Maintenance>(entity =>
        {
            entity.HasKey(e => e.MaintenanceId).HasName("PK__Maintena__9D754BEAC4E7B951");

            entity.ToTable("Maintenance");

            entity.Property(e => e.MaintenanceId).HasColumnName("maintenance_id");
            entity.Property(e => e.BicycleId).HasColumnName("bicycle_id");
            entity.Property(e => e.MaintenanceEndDate)
                .HasColumnType("datetime")
                .HasColumnName("maintenance_end_date");
            entity.Property(e => e.MaintenanceNotes)
                .HasColumnType("text")
                .HasColumnName("maintenance_notes");
            entity.Property(e => e.MaintenanceStartDate)
                .HasColumnType("datetime")
                .HasColumnName("maintenance_start_date");

            entity.HasOne(d => d.Bicycle).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.BicycleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("maintenance_bicycle_key");
        });

        modelBuilder.Entity<RentingProcess>(entity =>
        {
            entity.HasKey(e => e.ProcessId).HasName("PK__RentingP__9446C3E121620511");

            entity.Property(e => e.ProcessId).HasColumnName("process_id");
            entity.Property(e => e.BicycleId).HasColumnName("bicycle_id");
            entity.Property(e => e.EndStationId).HasColumnName("end_station_id");
            entity.Property(e => e.ReturnTime)
                .HasColumnType("datetime")
                .HasColumnName("return_time");
            entity.Property(e => e.StartStationId).HasColumnName("start_station_id");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Bicycle).WithMany(p => p.RentingProcesses)
                .HasForeignKey(d => d.BicycleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bicycle_key");

            entity.HasOne(d => d.EndStation).WithMany(p => p.RentingProcessEndStations)
                .HasForeignKey(d => d.EndStationId)
                .HasConstraintName("end_station_key");

            entity.HasOne(d => d.StartStation).WithMany(p => p.RentingProcessStartStations)
                .HasForeignKey(d => d.StartStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("start_station_key");

            entity.HasOne(d => d.User).WithMany(p => p.RentingProcesses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_key");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("PK__Stations__44B370E946241485");

            entity.Property(e => e.StationId).HasColumnName("station_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Location)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.NameOfStation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name_of_station");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83FD24996AD");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E61646A909CFA").IsUnique();

            entity.HasIndex(e => e.CellNumber, "UQ__Users__B1E028C787841757").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CellNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cell_number");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_type_key");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__UserType__2C000598D9F43E6B");

            entity.HasIndex(e => e.TypeName, "UQ__UserType__543C4FD9CB59C8E9").IsUnique();

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
