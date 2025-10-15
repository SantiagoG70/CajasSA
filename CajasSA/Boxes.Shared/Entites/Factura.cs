using System.ComponentModel.DataAnnotations;

namespace Boxes.Shared.Entites;

public class Factura
{
    public int Id { get; set; }

    [Display(Name = "Fecha de emision")]
    public DateTime Fecha { get; set; }

    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; } = null!;
}