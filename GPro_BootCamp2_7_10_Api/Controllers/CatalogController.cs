using GPro_BootCamp2_7_10_Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPro_BootCamp2_7_10_Api.Controllers { 

 
[ApiController]
[Route("api/v1/catalog")]
public class CatalogController : ControllerBase
{
    private readonly IProductService _products;
    private readonly ICategoryService _categories;

    public CatalogController(IProductService products, ICategoryService categories)
    {
        _products = products;
        _categories = categories;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 12,
        [FromQuery] string? search = null,
        [FromQuery] int? categoryId = null,
        [FromQuery] string? sort = null,
        [FromQuery] string? dir = null)
    {
        var (items, total) = await _products.GetPageAsync(page, pageSize, search, categoryId, sort, dir);
        return Ok(new { items, total, page, pageSize });
    }

    [HttpGet("products/{id:int}")]
    public async Task<IActionResult> GetProductDetails(int id)
    {
        var dto = await _products.GetDetailsAsync(id);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] string? search = null)
    {
        var (items, total) = await _categories.GetPageAsync(page, pageSize, search);
        return Ok(new { items, total, page, pageSize });
    }
}
}