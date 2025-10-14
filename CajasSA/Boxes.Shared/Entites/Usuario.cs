using System.ComponentModel.DataAnnotations;

namespace Boxes.Shared.Entites;

public class Usuario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo {0} debe de ser obligatorio")]
    [MaxLength(30, ErrorMessage = "el campo{0} no puede tener {1} caracteres")]
    public string Nombre { get; set; } = null!;
}