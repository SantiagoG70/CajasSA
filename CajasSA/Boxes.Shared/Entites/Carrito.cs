using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boxes.Shared.Entites;

public class Carrito
{
    public int Id { get; set; }

    [Display(Name = "Fecha de creacion")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public DateTime CreationDate { get; set; }

    [Display(Name = "Estado")]
    [StringLength(30, MinimumLength = 4, ErrorMessage = "el nombre dene tener entre 4 y 30 caracteres")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string State { get; set; } = null!;

    [ForeignKey("Cliente")]
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    public ICollection<ItemCarrito>? Items { get; set; }

    
    public Factura? Factura { get; set; }
}