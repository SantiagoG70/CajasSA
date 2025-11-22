using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Boxes.Shared.Entites;

public class Producto
{
    public int Id { get; set; }

    [Display(Name = "Nombre Producto")]
    [StringLength(50, MinimumLength = 3)]
    [Required]
    public string Name { get; set; } = null!;

    [Display(Name = "Cantidad del producto")]
    [Required]
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [Display(Name = "Precio")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public decimal Price { get; set; }


    [DataType(DataType.MultilineText)]
    [Display(Name = "Descripción")]
    [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
    public string Description { get; set; } = null!;


    [Display(Name = "Tipo del producto")]
    [StringLength(30, MinimumLength = 3)]
    [Required]
    public string Type { get; set; } = null!;

    [Display(Name = "Stock Máximo")]
    [Required]
    public int Max { get; set; }

    [Display(Name = "Stock Mínimo")]
    [Required]
    public int Min { get; set; }

    [ForeignKey("Proveedor")]
    public int ProveedorId { get; set; }

    public Proveedor? Proveedor { get; set; }

    [JsonIgnore]
    public ICollection<ItemCarrito>? ItemsCarrito { get; set; }

    [JsonIgnore]
    public ICollection<DetalleFactura>? DetallesFactura { get; set; }

    [ForeignKey("InventarioId")]
    public int? InventarioId { get; set; }
    public Inventario? Inventario { get; set; } // Relación uno a uno

    public ICollection<ProductCategory>? ProductCategories { get; set; }

    [Display(Name = "Categorías")]
    public int ProductCategoriesNumber => ProductCategories == null || ProductCategories.Count == 0 ? 0 : ProductCategories.Count;


    public ICollection<ProductImage>? ProductImages { get; set; }
     
    [Display(Name = "Imágenes")]
    public int ProductImagesNumber => ProductImages == null || ProductImages.Count == 0 ? 0 : ProductImages.Count;

    [Display(Name = "Imagén")]
    public string MainImage => ProductImages == null || ProductImages.Count == 0 ? string.Empty : ProductImages.FirstOrDefault()!.Image;

    public ICollection<OrdenTemporal>? TemporalOrders { get; set; }

    public ICollection<DetalleOrden>? OrderDetails { get; set; }
}