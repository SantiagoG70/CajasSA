namespace Boxes.Shared.Entites;

public class Administrador
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
}