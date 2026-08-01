using System.ComponentModel.DataAnnotations;

namespace VidrieriaPresupuestos.Domain.Entidades
{
    public class CargoAdicional
    {
        public int Id { get; set; }

        public int PresupuestoId { get; set; }

        [Required]
        public Presupuesto Presupuesto { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = null!;

        [Required]
        public TipoValorCargo TipoValor { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Valor { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal MontoCalculado { get; set; }

        public int Cantidad { get; set; } = 1;

        [StringLength(20)]
        public string? Unidad { get; set; } = "u.";
    }
}
