using System.ComponentModel.DataAnnotations.Schema;

namespace Boxes.Shared.Entites;

public class Producto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Weight { get; set; }

    public string Type { get; set; } = null!;

    public DateTime EntryDate { get; set; }

    public int Max { get; set; }

    public int Min { get; set; }

    [ForeignKey("Proveedor")]
    public int ProveedorId { get; set; } //foreign key

    public Proveedor? Proveedor { get; set; } = null!; //navigation property
}