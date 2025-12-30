using Microsoft.EntityFrameworkCore;
using TabletopNote.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TabletopNoteDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("Default"),
        sqlite => sqlite.MigrationsAssembly("TabletopNote.Data")
    );
});

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
