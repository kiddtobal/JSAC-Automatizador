using System.ComponentModel.DataAnnotations;

namespace VidrieriaPresupuestos.Domain.Entidades
{
    public class ProductoPrecioReferencia
    {
        public int Id { get; set; }

        public int ProductoCatalogoId { get; set; }

        [Required]
        public ProductoCatalogo ProductoCatalogo { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Proveedor { get; set; } = null!;

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Precio { get; set; }

        public string? UrlReferencia { get; set; }
    }
}
