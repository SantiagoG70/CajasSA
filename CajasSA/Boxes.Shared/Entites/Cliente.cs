using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes.Shared.Entites
{
    public class Cliente: Usuario
    {

        [Required(ErrorMessage = "La direccion es obligatoria")]
        public string Direccion { get; set; } = null!;

        public ICollection<Factura>? Facturas { get; set; } = null!;
    }
}
 