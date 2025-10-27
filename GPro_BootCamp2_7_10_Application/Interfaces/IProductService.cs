using GPro_BootCamp2_7_10_Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Application.Interfaces
{
    public interface IProductService
    {
       
        Task<(IEnumerable<ProductListDto> items, int total)> GetPagedAsync(
        int page, int pageSize, string? search, int? categoryId, string? sort, string? dir);

        Task<(IEnumerable<ProductListDto> items, int total)> GetDeletedPagedAsync(
            int page, int pageSize, string? search);

        Task<ProductDetailDto?> GetDetailsAsync(int id);
        Task<ProductUpdateDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(ProductCreateDto dto);
        Task UpdateAsync(ProductUpdateDto dto);
        Task SoftDeleteAsync(int id);
        Task RestoreAsync(int id);
    }
}
