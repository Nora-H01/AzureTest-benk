using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using backend_api.Data;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS (IMPORTANT pour Angular)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// OpenAPI / Scalar
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

// 👉 CORS doit être avant Authorization
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();