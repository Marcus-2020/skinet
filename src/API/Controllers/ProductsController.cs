using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productsRepo, 
    IGenericRepository<ProductBrand> productBrandsRepo, 
    IGenericRepository<ProductType> productTypesRepo,
    IMapper mapper)
    {
        _productsRepo = productsRepo;
        _productBrandRepo = productBrandsRepo;
        _productTypeRepo = productTypesRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts() 
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();

        var products = await _productsRepo.ListAsync(spec);

        return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
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
        
        return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
    }
}