using System.ComponentModel.DataAnnotations;

namespace VidrieriaPresupuestos.Domain.Entidades
{
    public class Cotizador
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [StringLength(150)]
        public string? Cargo { get; set; }

        [Required]
        [StringLength(20)]
        public string Celular { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = null!;
    }
}
