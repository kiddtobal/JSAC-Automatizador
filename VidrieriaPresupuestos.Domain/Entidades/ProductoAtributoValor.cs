using System.ComponentModel.DataAnnotations;

namespace VidrieriaPresupuestos.Domain.Entidades
{
    public class ProductoAtributoValor
    {
        public int Id { get; set; }

        public int ProductoCatalogoId { get; set; }

        [Required]
        public ProductoCatalogo ProductoCatalogo { get; set; } = null!;

        public int AtributoDefinicionId { get; set; }

        [Required]
        public AtributoDefinicion AtributoDefinicion { get; set; } = null!;

        [Required]
        public string Valor { get; set; } = null!;
    }
}
