namespace Boxes.Shared.Entites;

public class Proveedor
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public Producto ProductType { get; set; } = null!;

    public ICollection<Producto>? Productos { get; set; } = null!; // navigation property
}