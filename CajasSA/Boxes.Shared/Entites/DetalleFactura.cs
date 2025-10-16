
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boxes.Shared.Entites;

public class DetalleFactura
{
    public int Id { get; set; }

    [Display(Name = "Detalles Factura")]
    [Required]
    [StringLength(200, MinimumLength = 10)]
    public string Details { get; set; } = null!;

    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }

   
    [ForeignKey("Factura")]
    public int FacturaId { get; set; }
    public Factura? Factura { get; set; }

 
    [ForeignKey("Producto")]
    public int ProductoId { get; set; }
    public Producto? Producto { get; set; }
}
