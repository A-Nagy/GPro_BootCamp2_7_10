using GPro_BootCamp2_7_10_Application.DTOs;
using GPro_BootCamp2_7_10_Application.Interfaces;
using GPro_BootCamp2_7_10_Domain.Entities;
using GPro_BootCamp2_7_10_Infrastructure.Repositories;
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
        private readonly IUnitofWork _uow;

        public CategoryService(IRepository<Category> repo, IUnitofWork uow)
        {
            _repo = repo; _uow = uow;
        }

        public async Task<(IEnumerable<CategoryListDto> items, int total)> GetPagedAsync(int page, int pageSize, string? q)
        {
            var query = _repo.Query().AsNoTracking();
            if (!string.IsNullOrWhiteSpace(q)) query = query.Where(c => c.Name.Contains(q));
            var total = await query.CountAsync();
            var items = await query.OrderBy(c => c.Name)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .Select(c => new CategoryListDto(c.Id, c.Name, c.Description)).ToListAsync();
            return (items, total);
        }

        public async Task<(IEnumerable<CategoryListDto> items, int total)> GetDeletedPagedAsync(int page, int pageSize, string? q)
        {
            var query = _repo.QueryUnfiltered().Where(c => c.IsDeleted).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(q)) query = query.Where(c => c.Name.Contains(q));
            var total = await query.CountAsync();
            var items = await query.OrderBy(c => c.Name)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .Select(c => new CategoryListDto(c.Id, c.Name, c.Description)).ToListAsync();
            return (items, total);
        }

        public async Task<CategoryUpdateDto?> GetByIdAsync(int id)
        {
            var q = _repo.Query().AsNoTracking().Where(c => c.Id == id);
            return await q.Select(c => new CategoryUpdateDto(c.Id, c.Name, c.Description, c.RowVersion)).FirstOrDefaultAsync();
        }

        public async Task<int> CreateAsync(CategoryCreateDto dto)
        {
            var e = new Category { Name = dto.Name, Description = dto.Description };
            await _repo.AddAsync(e); await _uow.SaveChangesAsync(); return e.Id;
        }

        public async Task UpdateAsync(CategoryUpdateDto dto)
        {
            var e = await _repo.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException("Category not found");
            e.Name = dto.Name; e.Description = dto.Description; e.UpdatedAt = DateTime.UtcNow;
            _repo.Update(e); await _uow.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            var e = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Category not found");
            e.IsDeleted = true; e.UpdatedAt = DateTime.UtcNow;
            _repo.Update(e); await _uow.SaveChangesAsync();
        }

        public async Task RestoreAsync(int id)
        {
            var e = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Category not found");
            e.IsDeleted = false; e.UpdatedAt = DateTime.UtcNow;
            _repo.Update(e); await _uow.SaveChangesAsync();
        }
    }
}
