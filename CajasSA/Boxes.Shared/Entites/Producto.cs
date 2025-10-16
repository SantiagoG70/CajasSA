using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes.Shared.Entites
{
    public class Producto
    {
        public int Id { get; set; }

        [Display(Name = "Nombre Producto")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "el nombre dene tener entre 3 y 50 caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Cantidad del producto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int quantity { get; set; }

        [Display(Name = "Precio del producto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Price { get; set; }

        [Display(Name = "Peso del producto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Weight { get; set; }

        [Display(Name = "Tipo del producto")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "el tipo dene tener entre 3 y 30 caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Type { get; set; } = null!;

        [Display(Name = "Fecha de ingreso")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime EntryDate { get; set; }

        [Display(Name = "Stock Maximo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Max { get; set; }

        [Display(Name = "Stock Minimo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Min { get; set; }

        [ForeignKey("Proveedor")]
        public int ProveedorId { get; set; } //foreign key

        public Proveedor? Proveedor { get; set; } = null!; //navigation property

        public ItemCarrito? ItemCarrito { get; set; }

        // Relación inversa 1 a 1 con DetalleFactura
        public DetalleFactura? DetalleFactura { get; set; }
    }
}
