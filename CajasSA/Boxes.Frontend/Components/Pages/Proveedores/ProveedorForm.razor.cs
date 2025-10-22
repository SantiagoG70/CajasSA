using Microsoft.AspNetCore.Components;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Components.Forms;

namespace Boxes.Frontend.Components.Pages.Proveedores
{
    public partial class ProveedorForm
    {
        private EditContext editContext = null!;

        [EditorRequired, Parameter] public Employee Employee { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }

        protected override void OnParametersSet()
        {
            if (editContext is null || !ReferenceEquals(editContext.Model, Employee))
                editContext = new EditContext(Employee);
        }

        protected override void OnInitialized()
        {
            editContext = new(Employee);
        }
    }
}
