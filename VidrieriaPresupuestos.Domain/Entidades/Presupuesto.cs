using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace VidrieriaPresupuestos.Domain.Entidades
{
    public class Presupuesto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NumeroCotizacion { get; set; } = null!;

        public int ClienteId { get; set; }

        [Required]
        public Cliente Cliente { get; set; } = null!;

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public EstadoPresupuesto Estado { get; set; }

        public string? DescripcionTrabajo { get; set; }

        public string? Referencia { get; set; }

        [Required]
        [StringLength(150)]
        public string Local { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string DireccionTrabajo { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Comuna { get; set; } = null!;

        public int CotizadorId { get; set; }

        [Required]
        public Cotizador Cotizador { get; set; } = null!;

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Subtotal { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal ValorNeto { get; set; }

        public ICollection<ItemPresupuesto> Items { get; set; } = new List<ItemPresupuesto>();

        public ICollection<CargoAdicional> CargosAdicionales { get; set; } = new List<CargoAdicional>();
    }

    public enum EstadoPresupuesto
    {
        Pendiente,
        Aprobado,
        Rechazado,
        Vencido
    }
}
