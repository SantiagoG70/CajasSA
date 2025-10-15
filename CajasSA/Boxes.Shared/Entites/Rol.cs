using System.ComponentModel.DataAnnotations;

namespace Boxes.Shared.Entites;

public class Rol
{
    public int Id { get; set; }

    [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string Nombre { get; set; } = null!;

    public ICollection<Usuario>? Usuarios { get; set; } = null!;
}