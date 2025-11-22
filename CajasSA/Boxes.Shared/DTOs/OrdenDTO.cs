using Boxes.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes.Shared.DTOs
{
    public class OrdenDTO
    {
        public int Id { get; set; }

        public OrdenStatus OrdenStatus { get; set; }

        public string Remarks { get; set; } = string.Empty;

    }
}
