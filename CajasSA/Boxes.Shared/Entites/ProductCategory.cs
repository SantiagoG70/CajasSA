using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes.Shared.Entites
{
    public class ProductCategory
    {
        public int Id { get; set; }

        public Producto? Producto { get; set; }

        public int ProductoId { get; set; }

        public Categoria? Category { get; set; }

        public int CategoryId { get; set; }

    }
}
