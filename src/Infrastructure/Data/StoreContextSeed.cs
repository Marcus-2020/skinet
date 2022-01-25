using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    private static readonly string baseUrl = "../Infrastructure/Data/SeedData/";

    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            await SeedBrands(context);
            await SeedTypes(context);
            await SeedProducts(context);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex.Message);
        }
    }

    private static async Task SeedBrands(StoreContext context)
    {
        if (!context.ProductBrands.Any())
        {
            var brandsData = File.ReadAllText($"{baseUrl}brands.json");

            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

            foreach (var item in brands)
            {
                context.ProductBrands.Add(item);
            }

            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedTypes(StoreContext context)
    {
        if (!context.ProductTypes.Any())
        {
            var typesData = File.ReadAllText($"{baseUrl}types.json");

            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

            foreach (var item in types)
            {
                context.ProductTypes.Add(item);
            }

            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProducts(StoreContext context)
    {
        if (!context.Products.Any())
        {
            var productsData = File.ReadAllText($"{baseUrl}products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            foreach (var item in products)
            {
                context.Products.Add(item);
            }

            await context.SaveChangesAsync();
        }
    }
}
