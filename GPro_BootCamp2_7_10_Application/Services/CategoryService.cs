using GPro_BootCamp2_7_10_Application.DTOs;
using GPro_BootCamp2_7_10_Application.Interfaces;
using GPro_BootCamp2_7_10_Domain.Entities;
using GPro_BootCamp2_7_10_Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repo;
        private readonly IUnitofWork _unitOfWork;
        public CategoryService(IRepository<Category> repo, IUnitofWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateAsync(CategoryCreateDto dto)
        {
            var entity = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
            };
            await _repo.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<(IEnumerable<CategoryListDto> items, int total)> GetPageAsync(int page, int pageSize, string? search)
        {
            var query= _repo.Query().AsNoTracking();

            if (!string.IsNullOrEmpty(search))
                            query = query.Where(c => c.Name.Contains(search));

           var total = await query.CountAsync();

           var items = await query
                .OrderBy(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CategoryListDto(c.Id, c.Name, c.Description))
                .ToListAsync();
            return (items, total);
        }

        public async Task RestoreAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new Exception("Category not found");
            entity.IsDeleted = false;
            entity.DeletedAt = DateTime.UtcNow;
            _repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new Exception("Category not found");
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            _repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryUpdateDto dto)
        {
            var entity =await _repo.GetByIdAsync(dto.Id) ?? throw new Exception("Category not found");
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.UpdatedAt = DateTime.UtcNow;
            _repo.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
