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
            entity.HasKey(e => e.ChoreId).HasName("PK__ChoresTy__F0F94F344B16A808");

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.ChoresTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChoresTyp__IdCar__30F848ED");
        });

        modelBuilder.Entity<DriversCar>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.IdCar }).HasName("PK__DriversC__8772B4498FFE43EE");

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.DriversCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriversCa__IdCar__2E1BDC42");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.DriversCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriversCa__Statu__2C3393D0");

            entity.HasOne(d => d.User).WithMany(p => p.DriversCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriversCa__UserI__2D27B809");
        });

        modelBuilder.Entity<Pic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pic__3214EC07476AD5F7");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Pic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pic__Id__3A81B327");
        });

        modelBuilder.Entity<PicChore>(entity =>
        {
            entity.HasKey(e => e.PicId).HasName("PK__PicChore__B04A93C14515248B");

            entity.HasOne(d => d.Assignment).WithMany(p => p.PicChores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PicChores__Assig__37A5467C");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Report__32499E77E54A9A82");

            entity.HasOne(d => d.Chore).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__ChoreId__34C8D9D1");

            entity.HasOne(d => d.DriversCar).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__33D4B598");
        });

        modelBuilder.Entity<RequestCar>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__RequestC__33A8517AF618CB6C");

            entity.HasOne(d => d.Status).WithMany(p => p.RequestCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestCa__Statu__3E52440B");

            entity.HasOne(d => d.DriversCar).WithMany(p => p.RequestCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestCar__3D5E1FD2");
        });

        modelBuilder.Entity<StatusCar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StatusCa__3214EC079DA8103A");
        });

        modelBuilder.Entity<TableCar>(entity =>
        {
            entity.HasKey(e => e.IdCar).HasName("PK__TableCar__0FA78058F1829AA5");
        });

        modelBuilder.Entity<TableUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TableUse__3214EC077A04083F");

            entity.HasOne(d => d.CarOwner).WithMany(p => p.InverseCarOwner).HasConstraintName("FK__TableUser__CarOw__25869641");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
