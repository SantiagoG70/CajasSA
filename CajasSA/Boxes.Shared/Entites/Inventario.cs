using System.Text.Json.Serialization;

namespace Boxes.Shared.Entites;

public class Inventario
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Stock { get; set; }

    [JsonIgnore]
    public ICollection<Producto>? Productos { get; set; } = new List<Producto>();

    [JsonIgnore]
    public ICollection<Alerta>? Alertas { get; set; } = null!;
}