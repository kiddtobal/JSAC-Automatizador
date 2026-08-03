using System.ComponentModel.DataAnnotations;

namespace VidrieriaPresupuestos.Domain.Entidades
{
    public class RecordatorioSistema
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Clave { get; set; } = null!;

        public DateTime? UltimaVezMostrado { get; set; }
    }
}
