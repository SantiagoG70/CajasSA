namespace Boxes.Shared.Entites;

public class Inventario
{
    public int Id { get; set; }


    public string Name { get; set; } = null!;
    public int Stock { get; set; }
    public ICollection<Producto>? Productos { get; set; } = new List<Producto>();

    public ICollection<Alerta>? Alertas { get; set; } = null!;
}