using MudBlazor.Services;
using TabletopNote.UI.Clients;
using TabletopNote.UI.Components;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

builder.Services.AddHttpClient<DocumentsApiClient>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var apiBaseUrl = config["ApiBaseUrl"];
    client.BaseAddress = new Uri(apiBaseUrl!);
});

builder.Services.AddMudServices();

builder.Services.AddScoped<UserTimeService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapStaticAssets();

app.MapRazorComponents<App>();

app.Run();
