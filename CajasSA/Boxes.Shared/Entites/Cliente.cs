using System.ComponentModel.DataAnnotations;

namespace Boxes.Shared.Entites;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La direccion es obligatoria")]
    public string Direccion { get; set; } = null!;

    public ICollection<Factura>? Facturas { get; set; } = null!;
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
}