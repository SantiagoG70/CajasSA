using System.ComponentModel.DataAnnotations;

namespace Boxes.Shared.Entites;

public class DetalleFactura
{
    public int Id { get; set; }

    [Display(Name = "Detalles Factura")]
    [Required(ErrorMessage = "Los detalles son obligatorios")]
    [StringLength(200, MinimumLength = 10, ErrorMessage = "Fuera del Rango")]
    public string Details { get; set; } = null!;

    public decimal unit_price { get; set; }
    public decimal subtotal { get; set; }
    public decimal Total { get; set; }
    public int FacturaId { get; set; }
    public Factura? Factura { get; set; }
}