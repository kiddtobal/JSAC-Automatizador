using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace VidrieriaPresupuestos.Domain.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Phone]
        [StringLength(20)]
        public string? Telefono { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        public Genero Genero { get; set; }

        [StringLength(150)]
        public string? Cargo { get; set; }

        [StringLength(150)]
        public string? Empresa { get; set; }

        public ICollection<Presupuesto> Presupuestos { get; set; } = new List<Presupuesto>();
    }
}
