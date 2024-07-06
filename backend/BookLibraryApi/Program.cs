using System.Text.Json.Serialization;
using BookLibraryApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",builder=>builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddHttpClient();
builder.Services.AddScoped<DbInitializer>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await dbInitializer.Initialize();
}

app.MapControllers();
app.UseCors("AllowAnyOrigin");

app.Run(); 
