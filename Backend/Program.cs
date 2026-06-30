using Application.Abstractions;
using Application.Brand.UseCases;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(connection));

builder.Services.AddTransient<IRepository<BrandEntity>, BrandRepository>();
builder.Services.AddTransient<IUseCase<BrandEntity>, BrandUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Traer las marcas
app.MapGet("/brand", async (IUseCase<BrandEntity> useCase) =>
{
    var brands = await useCase.GetAllAsync();
    return brands;
}).WithName("getbrand");

// Agregar una marca
app.MapPost("/brand", async (IUseCase<BrandEntity> useCase, BrandEntity brand) => {
    await useCase.AddAsync(brand);
    return Results.Created();
}).WithName("addbrand");

// Prueba
app.MapGet("/test", () =>
{
    return "online";
}).WithName("test");

app.Run();
