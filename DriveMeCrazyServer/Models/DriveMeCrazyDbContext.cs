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

    public virtual DbSet<CarType> CarTypes { get; set; }

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
        modelBuilder.Entity<CarType>(entity =>
        {
            entity.HasKey(e => e.IdCarType).HasName("PK__CarType__16CCFD67AE235B8E");

            entity.Property(e => e.IdCarType).ValueGeneratedNever();
        });

        modelBuilder.Entity<ChoresType>(entity =>
        {
            entity.HasKey(e => e.ChoreId).HasName("PK__ChoresTy__F0F94F34CA4EB4DB");

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.ChoresTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChoresTyp__IdCar__33D4B598");
        });

        modelBuilder.Entity<DriversCar>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.IdCar }).HasName("PK__DriversC__8772B449E05E2E33");

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.DriversCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriversCa__IdCar__30F848ED");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.DriversCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriversCa__Statu__2F10007B");

            entity.HasOne(d => d.User).WithMany(p => p.DriversCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriversCa__UserI__300424B4");
        });

        modelBuilder.Entity<Pic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pic__3214EC07CB728C95");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Pic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pic__Id__3D5E1FD2");
        });

        modelBuilder.Entity<PicChore>(entity =>
        {
            entity.HasKey(e => e.PicId).HasName("PK__PicChore__B04A93C1E25F5AC0");

            entity.HasOne(d => d.Assignment).WithMany(p => p.PicChores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PicChores__Assig__3A81B327");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Report__32499E77DF496025");

            entity.HasOne(d => d.Chore).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__ChoreId__37A5467C");

            entity.HasOne(d => d.DriversCar).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__36B12243");
        });

        modelBuilder.Entity<RequestCar>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__RequestC__33A8517ACD8BB080");

            entity.HasOne(d => d.Status).WithMany(p => p.RequestCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestCa__Statu__412EB0B6");

            entity.HasOne(d => d.DriversCar).WithMany(p => p.RequestCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestCar__403A8C7D");
        });

        modelBuilder.Entity<StatusCar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StatusCa__3214EC074CC05682");
        });

        modelBuilder.Entity<TableCar>(entity =>
        {
            entity.HasKey(e => e.IdCar).HasName("PK__TableCar__0FA7805867878838");

            entity.Property(e => e.IdCar).ValueGeneratedNever();

            entity.HasOne(d => d.Owner).WithMany(p => p.TableCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TableCars__Owner__2C3393D0");

            entity.HasOne(d => d.Type).WithMany(p => p.TableCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TableCars__TypeI__2B3F6F97");
        });

        modelBuilder.Entity<TableUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TableUse__3214EC07B7C95185");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
