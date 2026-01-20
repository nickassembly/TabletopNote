using Microsoft.EntityFrameworkCore;
using TabletopNote.Data;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<TabletopNoteDbContext>(options =>
{
    options.UseSqlite(
        connectionString,
        sqlite => sqlite.MigrationsAssembly("TabletopNote.Data").CommandTimeout(30)
    );
});

var dataSource = new SqliteConnectionStringBuilder(connectionString).DataSource;
var dbDirectory = Path.GetDirectoryName(dataSource);

if (!string.IsNullOrEmpty(dbDirectory) && !Directory.Exists(dbDirectory))
{
    Directory.CreateDirectory(dbDirectory);
}

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Apply pending migrations at startup
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<TabletopNoteDbContext>();
db.Database.Migrate();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
