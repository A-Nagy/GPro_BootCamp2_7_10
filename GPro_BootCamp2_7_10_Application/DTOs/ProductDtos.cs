using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Application.DTOs
{
    public record ProductDetailDto(
        int Id,
        string Name,
        string? Description,
        decimal Price,
        string Currency,
        int Qty,
        int? reservedQty , 
        int? CategoryId,
        string CategoryName,
        int? SupplierId,
        string? SupplierName,
        List<string> ImageUrls,
        byte[] RowVersion);

    public record ProductCreateDto(
        string Name,
        string? Description,
        decimal Price,
        string Currency,
        int Qty,
        int CategoryId,
        int SupplierId
        );
    public record ProductUpdateDto(
        int Id,
        string Name,
        string? Description,
        decimal Price,
        string Currency,
        int Qty,
        int CategoryId,
        int SupplierId,
        byte[] RowVersion);
    public record ProductListDto(
        int Id,
        string Name,
        decimal Price,
        string Currency,
        int Qty,
        int? CategoryId,
        string CategoryName,
        string? MainImageUrl);
        

}
