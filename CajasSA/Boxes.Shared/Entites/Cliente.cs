using System.ComponentModel.DataAnnotations;

namespace Boxes.Shared.Entites;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La direccion es obligatoria")]
    [StringLength(100, MinimumLength = 10, ErrorMessage = "la direccion dene tener entre 10 y 100 caracteres")]
    public string Address { get; set; } = null!;

    public ICollection<Carrito>? Carritos { get; set; } // Relation with Carrito 1 ...*
    public ICollection<Factura>? Facturas { get; set; } = null!; // Relation with Factura 1 ...*
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
}