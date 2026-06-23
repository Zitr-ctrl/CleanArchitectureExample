using Application.Abstractions;
using Application.Brand.UseCases;
using Domain;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

// Conexion
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string connectionString = configuration.GetConnectionString("DefaultConnection");

var services = new ServiceCollection();
services.AddDbContext<StoreContext>(options =>
    options.UseSqlServer(connectionString));

services.AddScoped<IRepository<BrandEntity>, BrandRepository>();
services.AddScoped<IUseCase<BrandEntity>, BrandUseCase>();

var serviceProvider = services.BuildServiceProvider();

var brandUseCase = serviceProvider.GetRequiredService<IUseCase<BrandEntity>>();

while(true)
{
    try
    {
        Console.WriteLine("\nSelecciona una Opción:");
        Console.WriteLine("1.- Agregar una marca");
        Console.WriteLine("2.- Mostrar marcas almacenadas");
        Console.WriteLine("3.- Salir");
        Console.WriteLine("Opción: ");
        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Console.WriteLine("Ingrese una Marca");
                string name = Console.ReadLine();
                await brandUseCase.AddAsync(new BrandEntity(name));
                break;
            case "2":
                Console.WriteLine("\nMarcas Almacenadas");
                var brands = await brandUseCase.GetAllAsync();
                foreach (var brand in brands)
                {
                    Console.WriteLine($"Marca: {brand.Name}");
                }
                break;
            case "3":
                Console.WriteLine("Saliendo del sistema...");
                break;
            default:
                Console.WriteLine("Opcion Invalida");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ocurrió un error: {ex.Message}");
        Console.WriteLine(ex.StackTrace);
    }
}