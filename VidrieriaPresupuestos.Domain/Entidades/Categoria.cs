using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace VidrieriaPresupuestos.Domain.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        public int? CategoriaPadreId { get; set; }

        public Categoria? CategoriaPadre { get; set; }

        public ICollection<Categoria> Subcategorias { get; set; } = new List<Categoria>();
    }
}
