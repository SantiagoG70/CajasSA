using System.ComponentModel.DataAnnotations;

namespace Boxes.Shared.Entites;

public class ItemCarrito
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    [StringLength(30, MinimumLength = 4, ErrorMessage = "el nombre dene tener entre 4 y 30 caracteres")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string Name { get; set; } = null!;

    [Display(Name = "Cantidad")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public int Quantity { get; set; }

    [Display(Name = "Precio Unitario")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public double UnitPrice { get; set; }
}