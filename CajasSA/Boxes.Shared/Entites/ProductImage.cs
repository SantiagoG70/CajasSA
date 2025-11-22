using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes.Shared.Entites
{
    public class ProductImage
    {
        public int Id { get; set; }

        public Producto? Producto { get; set;}

        public int ProductId { get; set; }

        [Display(Name = "Imagen")]
        public string Image { get; set; } = null!;
    }

}

