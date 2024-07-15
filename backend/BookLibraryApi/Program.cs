using BookLibraryApi.Data;
using BookLibraryApi.Data.Repositories;
using BookLibraryApi.Interfaces.Repositories;
using BookLibraryApi.Interfaces.Services;
using BookLibraryApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);


builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",policy=>policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<DbInitializer>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBookService,BookService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await dbInitializer.InitializeAsync();
}

app.MapControllers();
app.UseCors("AllowAnyOrigin");

app.Run(); 
