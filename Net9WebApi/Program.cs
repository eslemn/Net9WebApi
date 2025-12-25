using Microsoft.EntityFrameworkCore;
using Net9WebApi.Data;
using Net9WebApi.Services.Interfaces;
using Net9WebApi.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

//
// 🔹 SERVICES
//

// Controller support
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext (örnek: SQL Server)
// Eğer SQLite / PostgreSQL kullanıyorsan burayı sonra değiştiririz
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")
)
);

// Service Layer (DI)
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

//
// 🔹 MIDDLEWARE PIPELINE
//

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// (JWT eklenince buraya Authentication gelecek)
// app.UseAuthentication();

app.UseAuthorization();

// Controller endpoint’leri
app.MapControllers();

app.Run();
