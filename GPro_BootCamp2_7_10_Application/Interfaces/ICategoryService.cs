using GPro_BootCamp2_7_10_Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Application.Interfaces
{
    public  interface ICategoryService
    {
        Task<(IEnumerable<CategoryListDto> items, int total)> GetPagedAsync(int page, int pageSize, string? search);
        Task<(IEnumerable<CategoryListDto> items, int total)> GetDeletedPagedAsync(int page, int pageSize, string? search);
        Task<CategoryUpdateDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CategoryCreateDto dto);
        Task UpdateAsync(CategoryUpdateDto dto);
        Task SoftDeleteAsync(int id);
        Task RestoreAsync(int id);

    }
}
