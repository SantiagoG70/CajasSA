using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes.Shared.Enums
{
    public enum OrdenStatus
    {
        [Description("Nuevo")]
        New,

        [Description("Despachado")]
        Dispatched,

        [Description("Enviado")]
        Sent,

        [Description("Confirmado")]
        Confirmed,

        [Description("Cancelado")]
        Cancelled

    }
}
