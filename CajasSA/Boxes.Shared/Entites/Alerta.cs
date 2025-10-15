using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes.Shared.Entites
{
    public class Alerta
    {
        public int Id { get; set; }

        [Display(Name = "Fecha Alerta")]
        public DateTime Date { get; set; }

        [Display(Name = "Descripcion Alerta")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "la descripcion dene tener entre 10 y 200 caracteres")]
        public string Description { get; set; } = null!;
    }
}
 