using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;

    public ProductsController(IGenericRepository<Product> productsRepo, 
    IGenericRepository<ProductBrand> productBrandsRepo, 
    IGenericRepository<ProductType> productTypesRepo)
    {
        _productsRepo = productsRepo;
        _productBrandRepo = productBrandsRepo;
        _productTypeRepo = productTypesRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts() 
    {
        var products = await _productsRepo.ListAllAsync();

        return Ok(products);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands() 
    {
        var brands = await _productBrandRepo.ListAllAsync();

        return Ok(brands);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes() 
    {
        var types = await _productTypeRepo.ListAllAsync();

        return Ok(types);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productsRepo.GetbyIdAsync(id);
        
        return Ok(product);
    }
}