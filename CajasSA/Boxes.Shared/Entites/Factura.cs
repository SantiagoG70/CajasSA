using System.ComponentModel.DataAnnotations;

namespace Boxes.Shared.Entites;

public class Factura
{
    public int Id { get; set; }

    [Display(Name = "Fecha de emision")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public DateTime Date { get; set; }

    public int CarritoId { get; set; }
    public Carrito? Carrito { get; set; }
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; } = null!;
}