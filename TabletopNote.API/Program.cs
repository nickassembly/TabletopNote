using Microsoft.EntityFrameworkCore;
using TabletopNote.Data;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("TableTopUI",
        policy => policy
            .WithOrigins("https://tabletopnotewebapp-dubhgkdcfpb0dugb.centralus-01.azurewebsites.net")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var connectionString = builder.Configuration.GetConnectionString("Default");
Console.WriteLine($"SQLite path in use: {connectionString}");

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

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

var app = builder.Build();

app.UseCors("TableTopUI");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
