using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Display(Name = "Precio del producto")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Peso del producto")]
        [Required]
        public decimal Weight { get; set; }

        [Display(Name = "Tipo del producto")]
        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Type { get; set; } = null!;

        [Display(Name = "Fecha de ingreso")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime EntryDate { get; set; }

        [Display(Name = "Stock Máximo")]
        [Required]
        public int Max { get; set; }

        [Display(Name = "Stock Mínimo")]
        [Required]
        public int Min { get; set; }

        [ForeignKey("Proveedor")]
        public int ProveedorId { get; set; }

        public Proveedor? Proveedor { get; set; }

        public ICollection<ItemCarrito>? ItemsCarrito { get; set; }

        public ICollection<DetalleFactura>? DetallesFactura { get; set; }

        public int InventarioId { get; set; }

        [ForeignKey("InventarioId")]
        public Inventario? Inventario { get; set; }

    }



