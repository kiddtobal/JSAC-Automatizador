using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace VidrieriaPresupuestos.Domain.Entidades
{
    public class Presupuesto
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        [Required]
        public Cliente Cliente { get; set; } = null!;

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public EstadoPresupuesto Estado { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Total { get; set; }

        public ICollection<ItemPresupuesto> Items { get; set; } = new List<ItemPresupuesto>();
    }

    public enum EstadoPresupuesto
    {
        Pendiente,
        Aprobado,
        Rechazado,
        Vencido
    }
}
