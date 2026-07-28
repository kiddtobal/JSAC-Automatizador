using Microsoft.EntityFrameworkCore;
using VidrieriaPresupuestos.Domain.Entidades;

namespace VidrieriaPresupuestos.Web.Data
{
    public class VidrieriaContext : DbContext
    {
        public VidrieriaContext(DbContextOptions<VidrieriaContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Presupuesto> Presupuestos { get; set; } = null!;
        public DbSet<ItemPresupuesto> ItemsPresupuesto { get; set; } = null!;
        public DbSet<CargoAdicional> CargosAdicionales { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<AtributoDefinicion> AtributosDefinicion { get; set; } = null!;
        public DbSet<ProductoCatalogo> ProductosCatalogo { get; set; } = null!;
        public DbSet<ProductoAtributoValor> ProductoAtributoValores { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Presupuesto>(entity =>
            {
                entity.HasOne(p => p.Cliente)
                    .WithMany(c => c.Presupuestos)
                    .HasForeignKey(p => p.ClienteId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.Subtotal)
                    .HasPrecision(10, 2);

                entity.Property(p => p.ValorNeto)
                    .HasPrecision(10, 2);
            });

            modelBuilder.Entity<ItemPresupuesto>(entity =>
            {
                entity.HasOne(i => i.Presupuesto)
                    .WithMany(p => p.Items)
                    .HasForeignKey(i => i.PresupuestoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(i => i.PrecioBase)
                    .HasPrecision(10, 2);

                entity.Property(i => i.PorcentajeComision)
                    .HasPrecision(10, 2);

                entity.Property(i => i.ValorUnitario)
                    .HasPrecision(10, 2);

                entity.Property(i => i.Total)
                    .HasPrecision(10, 2);
            });

            modelBuilder.Entity<CargoAdicional>(entity =>
            {
                entity.HasOne(c => c.Presupuesto)
                    .WithMany(p => p.CargosAdicionales)
                    .HasForeignKey(c => c.PresupuestoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(c => c.Valor)
                    .HasPrecision(10, 2);

                entity.Property(c => c.MontoCalculado)
                    .HasPrecision(10, 2);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasOne(c => c.CategoriaPadre)
                    .WithMany(c => c.Subcategorias)
                    .HasForeignKey(c => c.CategoriaPadreId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AtributoDefinicion>(entity =>
            {
                entity.HasOne(a => a.Categoria)
                    .WithMany()
                    .HasForeignKey(a => a.CategoriaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductoCatalogo>(entity =>
            {
                entity.HasOne(p => p.Categoria)
                    .WithMany()
                    .HasForeignKey(p => p.CategoriaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.PrecioBase)
                    .HasPrecision(10, 2);

                entity.Property(p => p.PorcentajeComision)
                    .HasPrecision(10, 2);
            });

            modelBuilder.Entity<ProductoAtributoValor>(entity =>
            {
                entity.HasOne(v => v.ProductoCatalogo)
                    .WithMany(p => p.ValoresAtributos)
                    .HasForeignKey(v => v.ProductoCatalogoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(v => v.AtributoDefinicion)
                    .WithMany()
                    .HasForeignKey(v => v.AtributoDefinicionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
