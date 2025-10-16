using System.ComponentModel.DataAnnotations;

namespace Boxes.Shared.Entites;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La direccion es obligatoria")]
    [StringLength(100, MinimumLength = 10, ErrorMessage = "La direccion debe tener entre 10 y 100 caracteres")]
    public string Direccion { get; set; } = null!;

    
    public ICollection<Carrito>? Carritos { get; set; } // 1 a N
    public ICollection<Factura>? Facturas { get; set; } // 1 a N

    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
}