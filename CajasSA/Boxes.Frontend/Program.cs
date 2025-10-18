using MudBlazor.Services;
using Boxes.Frontend.Components;
using Boxes.Frontend.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri("https://localhost:7117/") });
builder.Services.AddScoped<IRepository , Repository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();