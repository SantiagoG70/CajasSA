using Microsoft.AspNetCore.Components;

namespace Boxes.Frontend.Components.Pages.Shared
{
    public partial class GenericList<TItem> //this class will be used for all entities to list them
    {
        [Parameter] public RenderFragment? Loading { get; set; } // to show a loading indicator while data is being fetched

        [Parameter] public RenderFragment? NoRecords { get; set; } // to show a message when there are no records to display

        [EditorRequired, Parameter] public RenderFragment? Body { get; set; } = null;

        [EditorRequired, Parameter] public List<TItem> MyList { get; set; } = null!;


    }
}
