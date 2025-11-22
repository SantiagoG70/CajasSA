using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boxes.Shared.Entites;

public class ItemCarrito
{
    public int Id { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    public int CarritoId { get; set; }
    public Carrito? Carrito { get; set; }

    public int ProductoId { get; set; }
    public Producto? Producto { get; set; }
}