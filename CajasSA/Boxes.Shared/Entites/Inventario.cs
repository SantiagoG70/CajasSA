using System.Text.Json.Serialization;

namespace Boxes.Shared.Entites;

public class Inventario
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public ICollection<Producto>? Productos { get; set; } = new List<Producto>();
}