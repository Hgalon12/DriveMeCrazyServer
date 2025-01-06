using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

public partial class DriveMeCrazyDbContext : DbContext
{
    public DriveMeCrazyDbContext()
    {
    }

    public DriveMeCrazyDbContext(DbContextOptions<DriveMeCrazyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChoresType> ChoresTypes { get; set; }

    public virtual DbSet<DriversCar> DriversCars { get; set; }

    public virtual DbSet<Pic> Pics { get; set; }

    public virtual DbSet<PicChore> PicChores { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<RequestCar> RequestCars { get; set; }

    public virtual DbSet<StatusCar> StatusCars { get; set; }

    public virtual DbSet<TableCar> TableCars { get; set; }

    public virtual DbSet<TableUser> TableUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog= DriveMeCrazyDB;User ID=CarsAdminLogin;Password=Hg2501;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChoresType>(entity =>
        {
            entity.HasKey(e => e.ChoreId).HasName("PK__ChoresTy__F0F94F34C6126E24");

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.ChoresTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChoresTyp__IdCar__34C8D9D1");
        });

        modelBuilder.Entity<DriversCar>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.IdCar }).HasName("PK__DriversC__8772B4499C2E9321");

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.DriversCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriversCa__IdCar__31EC6D26");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.DriversCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriversCa__Statu__300424B4");

            entity.HasOne(d => d.User).WithMany(p => p.DriversCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriversCa__UserI__30F848ED");
        });

        modelBuilder.Entity<Pic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pic__3214EC0775EB9D5E");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Pic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pic__Id__3E52440B");
        });

        modelBuilder.Entity<PicChore>(entity =>
        {
            entity.HasKey(e => e.PicId).HasName("PK__PicChore__B04A93C154F52653");

            entity.HasOne(d => d.Assignment).WithMany(p => p.PicChores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PicChores__Assig__3B75D760");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Report__32499E770232124E");

            entity.HasOne(d => d.Chore).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__ChoreId__38996AB5");

            entity.HasOne(d => d.DriversCar).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__37A5467C");
        });

        modelBuilder.Entity<RequestCar>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__RequestC__33A8517ADB653387");

            entity.HasOne(d => d.Status).WithMany(p => p.RequestCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestCa__Statu__4222D4EF");

            entity.HasOne(d => d.DriversCar).WithMany(p => p.RequestCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestCar__412EB0B6");
        });

        modelBuilder.Entity<StatusCar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StatusCa__3214EC07EFD1D425");
        });

        modelBuilder.Entity<TableCar>(entity =>
        {
            entity.HasKey(e => e.IdCar).HasName("PK__TableCar__0FA7805839DC4A2C");
        });

        modelBuilder.Entity<TableUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TableUse__3214EC07FA11CF68");

            entity.HasOne(d => d.CarOwner).WithMany(p => p.InverseCarOwner).HasConstraintName("FK__TableUser__UserP__29572725");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
