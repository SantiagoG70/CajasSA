using Microsoft.AspNetCore.Components;
using Boxes.Shared.Entites;
using Microsoft.AspNetCore.Components.Forms;

namespace Boxes.Frontend.Components.Pages.Proveedores;

public partial class ProveedorForm
{
    private EditContext editContext = null!;

    [EditorRequired, Parameter] public Proveedor Proveedor { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }

    protected override void OnParametersSet()
    {
        if (editContext is null || !ReferenceEquals(editContext.Model, Proveedor))
            editContext = new EditContext(Proveedor);
    }

    protected override void OnInitialized()
    {
        editContext = new(Proveedor);
    }
}