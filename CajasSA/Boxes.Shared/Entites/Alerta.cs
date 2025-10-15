using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes.Shared.Entites
{
    public class Alerta
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; } = null!;
    }
}
 