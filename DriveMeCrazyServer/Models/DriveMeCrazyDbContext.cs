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
            entity.HasKey(e => e.IdCarType).HasName("PK__CarType__16CCFD67EBF0F604");

            entity.Property(e => e.IdCarType).ValueGeneratedNever();
        });

        modelBuilder.Entity<ChoresType>(entity =>
        {
            entity.HasKey(e => e.ChoreId).HasName("PK__ChoresTy__F0F94F34A751AE35");

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.ChoresTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChoresTyp__IdCar__32E0915F");
        });

        modelBuilder.Entity<DriversCar>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.IdCar }).HasName("PK__DriversC__8772B4490DF0851B");

            entity.HasOne(d => d.IdCarNavigation).WithMany(p => p.DriversCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriversCa__IdCar__300424B4");

            entity.HasOne(d => d.User).WithMany(p => p.DriversCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriversCa__UserI__2F10007B");
        });

        modelBuilder.Entity<Pic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pic__3214EC07F8F7D6F8");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Pic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pic__Id__3C69FB99");
        });

        modelBuilder.Entity<PicChore>(entity =>
        {
            entity.HasKey(e => e.PicId).HasName("PK__PicChore__B04A93C1E5E1C4DE");

            entity.HasOne(d => d.Assignment).WithMany(p => p.PicChores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PicChores__Assig__398D8EEE");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Report__32499E77E3180958");

            entity.HasOne(d => d.Chore).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__ChoreId__36B12243");

            entity.HasOne(d => d.DriversCar).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__35BCFE0A");
        });

        modelBuilder.Entity<RequestCar>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__RequestC__33A8517AC5C2F654");

            entity.HasOne(d => d.Status).WithMany(p => p.RequestCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestCa__Statu__403A8C7D");

            entity.HasOne(d => d.DriversCar).WithMany(p => p.RequestCars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RequestCar__3F466844");
        });

        modelBuilder.Entity<StatusCar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StatusCa__3214EC0708EB2D16");
        });

        modelBuilder.Entity<TableCar>(entity =>
        {
            entity.HasKey(e => e.IdCar).HasName("PK__TableCar__0FA780586930406F");

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
            entity.HasKey(e => e.Id).HasName("PK__TableUse__3214EC0752DB3C7F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
