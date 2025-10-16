using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boxes.Shared.Entites;

public class Carrito
{
    public int Id { get; set; }

    [Display(Name = "Fecha de creación")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public DateTime CreationDate { get; set; }

    [Display(Name = "Estado")]
    [StringLength(30, MinimumLength = 4)]
    [Required]
    public string State { get; set; } = null!;

 
    [ForeignKey("Cliente")]
    public int ClienteId { get; set; } //foreign key
    public Cliente? Cliente { get; set; } = null!; //navigation property

    public ICollection<ItemCarrito>? Items { get; set; } // Relation 1 ...* with ItemCarrito

    public Factura? Factura { get; set; } // Relation 1 to 1 with Factura
}