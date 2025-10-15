namespace Boxes.Shared.Entites;

public class Empleado
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
    
}