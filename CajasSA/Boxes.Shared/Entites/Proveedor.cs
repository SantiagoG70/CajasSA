using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes.Shared.Entites
{
    public class Proveedor
    {
        public int Id { get; set; }

        [Display(Name = "Nombre proveedor")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "el nombre dene tener entre 3 y 50 caracteres")]
        [Required]
        public string Name { get; set; } = null!;

        [Display(Name = "Telefono")]
        [Phone(ErrorMessage = "El número de teléfono no es válido.")]
        [StringLength(20)]
        [Required]
        public string Phone { get; set; } = null!;

        [Display(Name = "Direccion")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "la direccion dene tener entre 10 y 100 caracteres")]
        [Required]
        public string Address { get; set; } = null!;
        public Producto ProductType { get; set; } = null!;

    public ICollection<Producto>? Productos { get; set; } = null!; // navigation property
}