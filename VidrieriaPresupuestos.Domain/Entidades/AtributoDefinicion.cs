using System.ComponentModel.DataAnnotations;

namespace VidrieriaPresupuestos.Domain.Entidades
{
    public class AtributoDefinicion
    {
        public int Id { get; set; }

        public int CategoriaId { get; set; }

        [Required]
        public Categoria Categoria { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Required]
        public TipoDatoAtributo TipoDato { get; set; }
    }
}
