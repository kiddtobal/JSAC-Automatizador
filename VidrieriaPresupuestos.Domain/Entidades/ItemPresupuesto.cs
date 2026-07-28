using System.ComponentModel.DataAnnotations;

namespace VidrieriaPresupuestos.Domain.Entidades
{
    public class ItemPresupuesto
    {
        public int Id { get; set; }

        public int PresupuestoId { get; set; }

        [Required]
        public Presupuesto Presupuesto { get; set; } = null!;

        [Required]
        [StringLength(250)]
        public string Descripcion { get; set; } = null!;

        [StringLength(50)]
        public string? Categoria { get; set; }

        [Required]
        public OrigenItem Origen { get; set; }

        [Range(0, 9999999.9999)]
        public decimal? AnchoM { get; set; }

        [Range(0, 9999999.9999)]
        public decimal? AltoM { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal PrecioBase { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal PorcentajeComision { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal PrecioFinal { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Subtotal { get; set; }
    }
}
