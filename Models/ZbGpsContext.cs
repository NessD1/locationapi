using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace locationapi.Models;

public partial class ZbGpsContext : DbContext
{
    public ZbGpsContext()
    {
    }

    public ZbGpsContext(DbContextOptions<ZbGpsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccesoAdispositivo> AccesoAdispositivos { get; set; }

    public virtual DbSet<ConfiguraciónGeneral> ConfiguraciónGenerals { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DetallesDelVehículo> DetallesDelVehículos { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<InventarioActual> InventarioActuals { get; set; }

    public virtual DbSet<LocationHistory> LocationHistories { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=zartbit.database.windows.net;Database=zbGPS; user id=denis;password=nessD1zbsql;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccesoAdispositivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AccesosADispositivos");

            entity.ToTable("AccesoADispositivos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdDeDispositivoPermitido).HasColumnName("ID_DeDispositivoPermitido");
            entity.Property(e => e.IdDeUsuario).HasColumnName("ID_DeUsuario");

            entity.HasOne(d => d.IdDeUsuarioNavigation).WithMany(p => p.AccesoAdispositivos)
                .HasForeignKey(d => d.IdDeUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccesoADispositivos_Usuario");
        });

        modelBuilder.Entity<ConfiguraciónGeneral>(entity =>
        {
            entity.HasKey(e => e.IdDeCliente);

            entity.ToTable("ConfiguraciónGeneral");

            entity.Property(e => e.IdDeCliente)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID_DeCliente");
            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.IdDeClienteNavigation).WithOne(p => p.ConfiguraciónGeneral)
                .HasForeignKey<ConfiguraciónGeneral>(d => d.IdDeCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConfiguraciónGeneral_customer1");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("country_code");
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.LastName)
                .HasMaxLength(150)
                .HasColumnName("last_name");
            entity.Property(e => e.MobilePhone)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("mobile_phone");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.PlanDePago)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("plan de pago");
        });

        modelBuilder.Entity<DetallesDelVehículo>(entity =>
        {
            entity.HasKey(e => e.IdDeDispositivo);

            entity.ToTable("DetallesDelVehículo");

            entity.Property(e => e.IdDeDispositivo)
                .ValueGeneratedNever()
                .HasColumnName("ID_DeDispositivo");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Modelo).HasMaxLength(50);
            entity.Property(e => e.NombreAsignadoAlDispositivo).HasMaxLength(50);
            entity.Property(e => e.Propietario).HasMaxLength(50);
            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasOne(d => d.IdDeDispositivoNavigation).WithOne(p => p.DetallesDelVehículo)
                .HasForeignKey<DetallesDelVehículo>(d => d.IdDeDispositivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallesDelVehículo_device");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_customer_devices");

            entity.ToTable("device");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Imei)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("IMEI");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsHistoryActive).HasColumnName("is_history_active");
            entity.Property(e => e.LatitudActual).HasMaxLength(80);
            entity.Property(e => e.LongitudActual).HasMaxLength(80);
            entity.Property(e => e.NúmeroDeControl)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Devices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_devices_customer");
        });

        modelBuilder.Entity<InventarioActual>(entity =>
        {
            entity.HasKey(e => e.NumeroDeParte);

            entity.ToTable("Inventario actual");

            entity.Property(e => e.NumeroDeParte)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Numero de parte");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.EmpaquetadoExtrinseco)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Empaquetado Extrinseco");
            entity.Property(e => e.EmpaquetadoIntrinseco)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Empaquetado Intrinseco");
            entity.Property(e => e.Manufacturador)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Proveedor)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LocationHistory>(entity =>
        {
            entity.ToTable("location_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeviceId).HasColumnName("device_id");
            entity.Property(e => e.Latitude)
                .HasMaxLength(80)
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasMaxLength(80)
                .HasColumnName("longitude");
            entity.Property(e => e.NúmeroDeControl)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.Device).WithMany(p => p.LocationHistories)
                .HasForeignKey(d => d.DeviceId)
                .HasConstraintName("FK_location_history_device");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña).HasMaxLength(20);
            entity.Property(e => e.CorreoElectrónico)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.IdDelCliente).HasColumnName("ID_DelCliente");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreDeUsuario)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SubjectId).HasComputedColumnSql("(newid())", false);
            entity.Property(e => e.TeléfonoAlterno)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TeléfonoCelular)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDelClienteNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdDelCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_customer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
