using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;

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
        var spec = new ProductsWithTypesAndBrandsSpecification();

        var products = await _productsRepo.ListAsync(spec);

        return Ok(products.Select(product => new ProductToReturnDto 
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            PictureUrl = product.PictureUrl,
            Price = product.Price,
            ProductBrand = product.ProductBrand.Name,
            ProductType = product.ProductType.Name,
        }).ToList());
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
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        
        var product = await _productsRepo.GetEntityWithSpec(spec);
        
        return Ok(new ProductToReturnDto 
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            PictureUrl = product.PictureUrl,
            Price = product.Price,
            ProductBrand = product.ProductBrand.Name,
            ProductType = product.ProductType.Name,
        });
    }
}