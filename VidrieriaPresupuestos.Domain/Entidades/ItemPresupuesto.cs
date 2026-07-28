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

        [Required]
        [StringLength(50)]
        public string TipoVidrio { get; set; } = null!;

        [Required]
        [Range(0, 9999999.9999)]
        public decimal AnchoM { get; set; }

        [Required]
        [Range(0, 9999999.9999)]
        public decimal AltoM { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal PrecioM2 { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Subtotal { get; set; }
    }
}
