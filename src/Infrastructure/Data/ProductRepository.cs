using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _context;
    public ProductRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        var products = await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .ToListAsync();

        return products;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductsBrandsAsync()
    {
        var brands = await _context.ProductBrands.ToListAsync();

        return brands;
    }

    public async Task<IReadOnlyList<ProductType>> GetProductsTypesAsync()
    {
        var types = await _context.ProductTypes.ToListAsync();

        return types;
    }
}
