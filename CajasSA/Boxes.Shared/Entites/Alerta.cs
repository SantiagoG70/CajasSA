using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Boxes.Shared.Entites;

public class Alerta
{
    public int Id { get; set; }

    [Display(Name = "Fecha Alerta")]
    public DateTime Date { get; set; }

    [Display(Name = "Descripcion Alerta")]
    [StringLength(200, MinimumLength = 10, ErrorMessage = "la descripcion dene tener entre 10 y 200 caracteres")]
    public string Description { get; set; } = null!;

    [ForeignKey("Inventario")]
    public int InventarioId { get; set; }
    public Inventario? Inventario { get; set; }
}