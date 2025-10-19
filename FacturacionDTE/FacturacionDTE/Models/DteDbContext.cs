using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FacturacionDTE.Models;

public partial class DteDbContext : DbContext
{
    public DteDbContext()
    {
    }

    public DteDbContext(DbContextOptions<DteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bitacora> Bitacoras { get; set; }

    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleDocumento> DetalleDocumentos { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Emisor> Emisors { get; set; }

    public virtual DbSet<Extension> Extensions { get; set; }

    public virtual DbSet<Firma> Firmas { get; set; }

    public virtual DbSet<Impuesto> Impuestos { get; set; }

    public virtual DbSet<MedioPago> MedioPagos { get; set; }

    public virtual DbSet<Mensaje> Mensajes { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Resuman> Resumen { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=DTE_DB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bitacora>(entity =>
        {
            entity.HasKey(e => e.BitacoraId).HasName("PK__Bitacora__7ACF9B38AD82CC1B");

            entity.ToTable("Bitacora");

            entity.Property(e => e.Accion).HasMaxLength(300);
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Bitacoras)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Bitacora_Usuario");
        });

        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1E52049472C");

            entity.ToTable("CategoriaProducto");

            entity.Property(e => e.Descripcion).HasMaxLength(300);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Cliente__71ABD0877248B426");

            entity.ToTable("Cliente");

            entity.HasIndex(e => e.Nit, "IDX_Cliente_NIT");

            entity.Property(e => e.Direccion).HasMaxLength(300);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nit)
                .HasMaxLength(17)
                .HasColumnName("NIT");
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Nrc)
                .HasMaxLength(8)
                .HasColumnName("NRC");
            entity.Property(e => e.Telefono).HasMaxLength(30);
        });

        modelBuilder.Entity<DetalleDocumento>(entity =>
        {
            entity.HasKey(e => e.DetalleId).HasName("PK__DetalleD__6E19D6DA2313961C");

            entity.ToTable("DetalleDocumento");

            entity.HasIndex(e => e.DocumentId, "IDX_Detalle_DocumentId");

            entity.Property(e => e.Cantidad)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SubTotal)
                .HasComputedColumnSql("([Cantidad]*[PrecioUnitario])", false)
                .HasColumnType("decimal(37, 4)");

            entity.HasOne(d => d.Document).WithMany(p => p.DetalleDocumentos)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleDocumento_Document");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleDocumentos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleDocumento_Producto");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF0F6017767E");

            entity.ToTable("Document");

            entity.HasIndex(e => e.ClienteId, "IDX_Document_ClienteId");

            entity.HasIndex(e => e.FechaEmision, "IDX_Document_Fecha");

            entity.HasIndex(e => e.NumeroConsecutivo, "IDX_Document_Numero");

            
            //entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaEmision).HasColumnType("datetime");
            entity.Property(e => e.Moneda).HasMaxLength(10);
            entity.Property(e => e.NumeroConsecutivo).HasMaxLength(50);
            entity.Property(e => e.NumeroFactura).HasMaxLength(50);
            entity.Property(e => e.Observaciones).HasMaxLength(500);
            entity.Property(e => e.TipoDocumento).HasMaxLength(50);
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Documents)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK_Document_Cliente");

            entity.HasOne(d => d.Emisor).WithMany(p => p.Documents)
                .HasForeignKey(d => d.EmisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Document_Emisor");
        });

        modelBuilder.Entity<Emisor>(entity =>
        {
            entity.HasKey(e => e.EmisorId).HasName("PK__Emisor__E03A4804778C248A");

            entity.ToTable("Emisor");

            entity.HasIndex(e => e.Nit, "IDX_Emisor_NIT");

            entity.Property(e => e.Direccion).HasMaxLength(300);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nit)
                .HasMaxLength(17)
                .HasColumnName("NIT");
            entity.Property(e => e.Nombre).HasMaxLength(250);
            entity.Property(e => e.Nrc)
                .HasMaxLength(8)
                .HasColumnName("NRC");
            entity.Property(e => e.Telefono).HasMaxLength(30);
        });

        modelBuilder.Entity<Extension>(entity =>
        {
            entity.HasKey(e => e.ExtensionId).HasName("PK__Extensio__5581AF2C7D02CB26");

            entity.ToTable("Extension");

            entity.Property(e => e.Nombre).HasMaxLength(200);

            entity.HasOne(d => d.Document).WithMany(p => p.Extensions)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Extension_Document");
        });

        modelBuilder.Entity<Firma>(entity =>
        {
            entity.HasKey(e => e.FirmaId).HasName("PK__Firma__CD9C5E2FBE019345");

            entity.ToTable("Firma");

            entity.Property(e => e.FechaFirma).HasColumnType("datetime");
            entity.Property(e => e.FirmaXml).HasColumnName("FirmaXML");

            entity.HasOne(d => d.Document).WithMany(p => p.Firmas)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Firma_Document");
        });

        modelBuilder.Entity<Impuesto>(entity =>
        {
            entity.HasKey(e => e.ImpuestoId).HasName("PK__Impuesto__CD9F45FE459F1E78");

            entity.ToTable("Impuesto");

            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Tasa).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Tipo).HasMaxLength(100);

            entity.HasOne(d => d.Detalle).WithMany(p => p.Impuestos)
                .HasForeignKey(d => d.DetalleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Impuesto_Detalle");
        });

        modelBuilder.Entity<MedioPago>(entity =>
        {
            entity.HasKey(e => e.MedioPagoId).HasName("PK__MedioPag__6D5407EEA313BA71");

            entity.ToTable("MedioPago");

            entity.Property(e => e.Banco).HasMaxLength(200);
            entity.Property(e => e.CodigoMedio).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Monto)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NumeroOperacion).HasMaxLength(200);

            entity.HasOne(d => d.Document).WithMany(p => p.MedioPagos)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedioPago_Document");
        });

        modelBuilder.Entity<Mensaje>(entity =>
        {
            entity.HasKey(e => e.MensajeId).HasName("PK__Mensaje__FEA0555F52664462");

            entity.ToTable("Mensaje");

            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Texto).HasMaxLength(1000);

            entity.HasOne(d => d.Document).WithMany(p => p.Mensajes)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mensaje_Document");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AEA31D7173B5");

            entity.ToTable("Producto");

            entity.HasIndex(e => e.Codigo, "IDX_Producto_Codigo");

            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoItem).HasDefaultValue(1);
            entity.Property(e => e.UnidadMedida).HasMaxLength(50);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK_Producto_Categoria");
        });

        modelBuilder.Entity<Resuman>(entity =>
        {
            entity.HasKey(e => e.ResumenId).HasName("PK__Resumen__03158145B340698A");

            entity.Property(e => e.MontoExento)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MontoGrabado)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MontoImpuestos)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalPagado)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalVenta)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Document).WithMany(p => p.Resumen)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resumen_Document");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B8B856632C");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuario__6B0F5AE0AA858439").IsUnique();

            entity.Property(e => e.ClaveHash).HasMaxLength(500);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.NombreUsuario).HasMaxLength(100);
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .HasDefaultValue("Empleado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
