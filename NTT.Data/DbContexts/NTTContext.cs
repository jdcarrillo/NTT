using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NTT.Entities.Models;

#nullable disable

namespace NTT.Entities.DbContexts
{
    public partial class NTTContext : DbContext
    {
        public NTTContext(DbContextOptions<NTTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<CuentaMovimiento> CuentaMovimientos { get; set; }
        public virtual DbSet<Cuenta> Cuenta { get; set; }
        public virtual DbSet<Movimiento> Movimientos { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("cliente");

                entity.Property(e => e.ClienteId).HasColumnName("cliente_id");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.PersonaId).HasColumnName("persona_id");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_persona_cliente");
            });

            modelBuilder.Entity<CuentaMovimiento>(entity =>
            {
                entity.ToTable("cuenta_movimiento");

                entity.Property(e => e.CuentaMovimientoId).HasColumnName("cuenta_movimiento_id");

                entity.Property(e => e.CuentaId).HasColumnName("cuenta_id");

                entity.Property(e => e.Movimiento)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("movimiento");

                entity.Property(e => e.MovimientoId).HasColumnName("movimiento_id");

                entity.Property(e => e.Saldo)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("saldo");

                entity.HasOne(d => d.Cuenta)
                    .WithMany(p => p.CuentaMovimientos)
                    .HasForeignKey(d => d.CuentaId)
                    .HasConstraintName("fk_cuen_cuenta_movimiento");

                entity.HasOne(d => d.MovimientoNavigation)
                    .WithMany(p => p.CuentaMovimientos)
                    .HasForeignKey(d => d.MovimientoId)
                    .HasConstraintName("fk_mov_cuenta_movimiento");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.CuentaId)
                    .HasName("PK_cuenta_1");

                entity.ToTable("cuenta");

                entity.Property(e => e.CuentaId).HasColumnName("cuenta_id");

                entity.Property(e => e.ClienteId).HasColumnName("cliente_id");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.NumeroCuenta)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("numero_cuenta");

                entity.Property(e => e.SaldoInicial)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("saldo_inicial");

                entity.Property(e => e.TipoCuenta)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tipo_cuenta");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cliente_cuenta");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.ToTable("movimiento");

                entity.Property(e => e.MovimientoId).HasColumnName("movimiento_id");

                entity.Property(e => e.CuentaId).HasColumnName("cuenta_id");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Saldo)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("saldo");

                entity.Property(e => e.TipoMovimiento)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tipo_movimiento");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("persona");

                entity.Property(e => e.PersonaId).HasColumnName("persona_id");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("genero");

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("identificacion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
