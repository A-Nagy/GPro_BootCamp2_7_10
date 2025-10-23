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
        Task<(IEnumerable<CategoryListDto> items ,int total )>  GetPageAsync(int page,int pageSize,string? search );
        Task<int> CreateAsync(CategoryCreateDto dto);
        Task UpdateAsync(CategoryUpdateDto dto);
        Task SoftDeleteAsync(int id);
        Task RestoreAsync(int id);

    }
}
