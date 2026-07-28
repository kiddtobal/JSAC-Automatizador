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

        [Required]
        public SeccionItem Seccion { get; set; }

        [Required]
        [StringLength(20)]
        public string Unidad { get; set; } = null!;

        [Required]
        [Range(0, 9999999999.99)]
        public decimal PrecioBase { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal PorcentajeComision { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal ValorUnitario { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Total { get; set; }
    }
}
