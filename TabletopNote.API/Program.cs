using Microsoft.EntityFrameworkCore;
using TabletopNote.Data;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("TableTopUI",
        policy => policy
            .WithOrigins("tabletopnote-api-h5gmcthkhqbeamdm.centralus-01.azurewebsites.net")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

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

var app = builder.Build();

app.UseCors("TableTopUI");

// Apply pending migrations at startup
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<TabletopNoteDbContext>();
db.Database.Migrate();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
