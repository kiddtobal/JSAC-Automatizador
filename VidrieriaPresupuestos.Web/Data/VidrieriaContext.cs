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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Presupuesto>(entity =>
            {
                entity.HasOne(p => p.Cliente)
                    .WithMany(c => c.Presupuestos)
                    .HasForeignKey(p => p.ClienteId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.Total)
                    .HasPrecision(10, 2);
            });

            modelBuilder.Entity<ItemPresupuesto>(entity =>
            {
                entity.HasOne(i => i.Presupuesto)
                    .WithMany(p => p.Items)
                    .HasForeignKey(i => i.PresupuestoId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(i => i.AnchoM)
                    .HasPrecision(10, 4);

                entity.Property(i => i.AltoM)
                    .HasPrecision(10, 4);

                entity.Property(i => i.PrecioM2)
                    .HasPrecision(10, 2);

                entity.Property(i => i.Subtotal)
                    .HasPrecision(10, 2);
            });
        }
    }
}
