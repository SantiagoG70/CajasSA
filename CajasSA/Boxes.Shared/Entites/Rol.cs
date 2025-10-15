using Boxes.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Boxes.Shared.Entites;

public class Rol : IEntityWithName
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    [StringLength(30, MinimumLength = 4, ErrorMessage = "el nombre dene tener entre 4 y 30 caracteres")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string Name { get; set; } = null!;

    public ICollection<Usuario>? Usuarios { get; set; } = null!;
}