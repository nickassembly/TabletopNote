using MudBlazor.Services;
using TabletopNote.UI.Clients;
using TabletopNote.UI.Components;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

builder.Services.AddMudServices();

builder.Services.AddHttpClient<DocumentsApiClient>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var apiBaseUrl = config["ApiBaseUrl"];
    client.BaseAddress = new Uri(apiBaseUrl!);
});

builder.Services.AddScoped<UserTimeService>();

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// HTTPS Redirection only if HTTPS port is available
var httpsPort = app.Urls.Select(url => new Uri(url))
                        .FirstOrDefault(u => u.Scheme == "https")?.Port;

if (httpsPort.HasValue)
{
    app.UseHttpsRedirection();
}

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>();

app.Run();
