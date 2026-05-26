using Microsoft.EntityFrameworkCore;
using FSL;
using FSL.Entities;
using FSL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// CORS policy: AllowLocal -> origine http://localhost, tutti i metodi e header
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocal", policy =>
    {
        policy.WithOrigins("http://localhost").AllowAnyMethod().AllowAnyHeader();
    });
});

// DbContext con SQL Server (usa la connection string "DefaultConnection" in appsettings)
builder.Services.AddDbContext<ZeusContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrazione servizio scoped
builder.Services.AddScoped<IServiceScontrini, ServiceScontrini>();

// OpenAPI solo in Development
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddOpenApi();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocal");

app.UseAuthorization();

app.MapControllers();

app.Run();
