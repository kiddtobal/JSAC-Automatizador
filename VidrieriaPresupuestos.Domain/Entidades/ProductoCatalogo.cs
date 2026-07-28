using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace VidrieriaPresupuestos.Domain.Entidades
{
    public class ProductoCatalogo
    {
        public int Id { get; set; }

        public int CategoriaId { get; set; }

        [Required]
        public Categoria Categoria { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Unidad { get; set; } = null!;

        [Required]
        public OrigenItem Origen { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal PrecioBase { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal PorcentajeComision { get; set; }

        public bool Activo { get; set; } = true;

        public ICollection<ProductoAtributoValor> ValoresAtributos { get; set; } = new List<ProductoAtributoValor>();
    }
}
