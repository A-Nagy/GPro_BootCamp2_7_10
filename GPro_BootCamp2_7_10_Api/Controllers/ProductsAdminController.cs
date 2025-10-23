using GPro_BootCamp2_7_10_Application.DTOs;
using GPro_BootCamp2_7_10_Application.Interfaces;
using GPro_BootCamp2_7_10_Application.DTOs;
using GPro_BootCamp2_7_10_Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace GPro_BootCamp2_7_10_Api.Controllers { 

 
[ApiController]
[Route("api/v1/products")]
public class ProductsAdminController : ControllerBase
{
    private readonly IProductService _products;
    private static readonly string[] AllowedExt = new[] { ".jpg", ".jpeg", ".png", ".webp" };
    private const long MaxSizeBytes = 2 * 1024 * 1024; // 2MB

    public ProductsAdminController(IProductService products)
    {
        _products = products;
    }

    [HttpPost]
    [Authorize(Policy = "Product.Write")]
    public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
    {
        var id = await _products.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = "Product.Read")]
    public async Task<IActionResult> GetById(int id)
    {
        var dto = await _products.GetDetailsAsync(id);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "Product.Write")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto dto)
    {
        if (id != dto.Id) return BadRequest("Mismatched id");
        await _products.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "Product.Write")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        await _products.SoftDeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{id:int}/restore")]
    [Authorize(Policy = "Product.Write")]
    public async Task<IActionResult> Restore(int id)
    {
        await _products.RestoreAsync(id);
        return NoContent();
    }

    [HttpPost("{id:int}/images")]
    [Authorize(Policy = "Product.Write")]
    [RequestSizeLimit(MaxSizeBytes)]
    public async Task<IActionResult> UploadImage(int id, IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("Empty file");
        if (file.Length > MaxSizeBytes) return BadRequest("Max 2MB");

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedExt.Contains(ext)) return BadRequest("Invalid extension");

         if (file.ContentType is null || (!file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase)))
            return BadRequest("Invalid content type");

        // مسار التخزين: wwwroot/uploads/products/yyyy/MM/
        var now = DateTime.UtcNow;
        var sub = Path.Combine("uploads", "products",
            now.ToString("yyyy", CultureInfo.InvariantCulture),
            now.ToString("MM", CultureInfo.InvariantCulture));

        var root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", sub);
        Directory.CreateDirectory(root);

        var fileName = $"{Guid.NewGuid():N}{ext}";
        var fullPath = Path.Combine(root, fileName);

        using (var stream = System.IO.File.Create(fullPath))
        {
            await file.CopyToAsync(stream);
        }

         var absUrl = $"{Request.Scheme}://{Request.Host}/{sub.Replace("\\", "/")}/{fileName}";
        return Ok(new { url = absUrl, fileName });
    }
}
}