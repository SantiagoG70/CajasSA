using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boxes.Shared.Entites;

public class ItemCarrito
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    [StringLength(30, MinimumLength = 4)]
    [Required]
    public string Name { get; set; } = null!;

    [Display(Name = "Cantidad")]
    [Required]
    public int Quantity { get; set; }

    [Display(Name = "Precio Unitario")]
    [Required]
    public double UnitPrice { get; set; }

   
    [ForeignKey("Carrito")]
    public int CarritoId { get; set; }
    public Carrito? Carrito { get; set; }

    
    [ForeignKey("Producto")]
    public int ProductoId { get; set; }
    public Producto? Producto { get; set; }
}