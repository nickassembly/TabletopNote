using MudBlazor.Services;
using TabletopNote.UI.Clients;
using TabletopNote.UI.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();
builder.Services.AddMudServices();

builder.Services.AddHttpClient<DocumentsApiClient>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(config["ApiBaseUrl"]!);
});

builder.Services.AddScoped<UserTimeService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

var httpsPort = app.Urls.Select(url => new Uri(url))
                        .FirstOrDefault(u => u.Scheme == "https")?.Port;

if (httpsPort.HasValue)
{
    app.UseHttpsRedirection();
}

app.MapStaticAssets();
app.MapRazorComponents<App>();

app.Run();
