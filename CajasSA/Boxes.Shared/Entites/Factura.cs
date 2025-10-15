using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes.Shared.Entites
{
    public class Factura
    {
        public int Id { get; set; }

        [Display(Name = "Fecha de emision")]
        public DateTime Fecha { get; set; } 
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; } = null!;
    }
}
