using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boxes.Shared.Entites;

public class Factura
{
    public int Id { get; set; }

    [Display(Name = "Fecha de emision")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public DateTime Date { get; set; }

    [ForeignKey("Carrito")]
    public int CarritoId { get; set; } // foreign key

    public Carrito? Carrito { get; set; } = null!; // navigation property

    [ForeignKey("Cliente")]
    public int ClienteId { get; set; } // foreign key 
    public Cliente? Cliente { get; set; } = null!; // navigation property

    public ICollection<DetalleFactura>? Detalles { get; set; }
}



   

   
   
