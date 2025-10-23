using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Application.DTOs
{

    public record CategoryCreateDto(string Name, string? Description);
    public record CategoryUpdateDto(int Id, string Name, string? Description, byte[] RowVersion);
    public record CategoryListDto(int Id, string Name , string? Description);

  
}
