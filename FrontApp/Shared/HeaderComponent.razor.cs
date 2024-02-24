using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.JSInterop;
namespace FrontApp.Shared;

public partial class HeaderComponent : IAsyncDisposable
{
    //declares
    private IJSObjectReference? module;

    //injections
    [Inject]
    public IJSRuntime JS { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            module = await JS.InvokeAsync<IJSObjectReference>(
                "import", "./Shared/HeaderComponent.razor.js");

            await module.InvokeVoidAsync("HeaderComponent.Alert()");

        }
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (module is not null)
        {
            await module.DisposeAsync();
        }
    }

}


