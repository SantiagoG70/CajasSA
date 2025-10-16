using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boxes.Shared.Entites;

public class Factura
{
    public int Id { get; set; }

    [Display(Name = "Fecha de emisión")]
    [Required]
    public DateTime Date { get; set; }

    [ForeignKey("Carrito")]
    public int CarritoId { get; set; }
    public Carrito? Carrito { get; set; }

    [ForeignKey("Cliente")]
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    public ICollection<DetalleFactura>? Detalles { get; set; }
}