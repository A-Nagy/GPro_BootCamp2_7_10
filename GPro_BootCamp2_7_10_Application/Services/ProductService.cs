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
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repo;
        private readonly IRepository<Category> _catrepo;
        private readonly IRepository<Supplier> _suprepo;
        private readonly IUnitofWork _uow; 
        public ProductService(IRepository<Product> repo, IRepository<Category> catrepo, IRepository<Supplier> suprepo, IUnitofWork uow)
        {
            _repo = repo;
            _catrepo = catrepo;
            _suprepo = suprepo;
            _uow = uow;
        }

        public async Task<int> CreateAsync(ProductCreateDto dto)
        {
            var entity = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Currency = dto.Currency,
                Qty = dto.Qty,
                SupplierId = dto.SupplierId,
                CategoryId = dto.CategoryId,
            };
            await _repo.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<ProductDetailDto?> GetDetailsAsync(int id)
        {
            return await _repo.Query().AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailDto
                (    p.Id,
                     p.Name,
                     p.Description,
                     p.Price,
                     p.Currency,
                     p.Qty,
                     p.ReservedQty,
                     p.CategoryId,
                     p.Category.Name,
                     p.SupplierId,
                     p.Supplier !=null ?p.Supplier.Name : null ,
                     p.Images.OrderByDescending(i=>i.IsMain).Select(i=>i.FileName).ToList(),
                     p.RowVersion
                    
                ))
                .FirstOrDefaultAsync();


        }

        public async Task<(IEnumerable<ProductListDto> items, int total)> GetPageAsync(int page, int pageSize, string? search, int? categoryId, string? sort, string? dir)
        {
            var query = _repo.Query().AsNoTracking();

             if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.Contains(search) || 
                                   (p.Description ?? "").Contains(search));

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

             var total = await query.CountAsync();

             bool desc = string.Equals(dir, "desc", StringComparison.OrdinalIgnoreCase);

            query = (sort?.ToLower()) switch
            {
                "Price" => (desc ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price)),
                "Qty" => (desc ? query.OrderByDescending(p => p.Qty) : query.OrderBy(p => p.Qty)),
                _ => (desc ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name)),
            };

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductListDto(
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Currency,
                    p.Qty,
                    p.CategoryId,
                    p.Category.Name,
                    p.Images.Where(i => i.IsMain).Select(i =>i.FileName).FirstOrDefault()
                ))
                .ToListAsync();

            return (items, total);
        }

        public async Task RestoreAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id) ?? throw new Exception("Product Is not Founded");
            entity.IsDeleted = false;
            entity.DeletedAt = DateTime.UtcNow;
            _repo.Update(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
             var entity =await _repo.GetByIdAsync(id) ?? throw new Exception("Product Is not Founded");
             entity.IsDeleted = true;
             entity.DeletedAt = DateTime.UtcNow;
            _repo.Update(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.Id) ?? throw new Exception("Product Is not Founded");

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Price = dto.Price;
            entity.Currency = dto.Currency;
            entity.Qty= dto.Qty;
            entity.SupplierId = dto.SupplierId;
            entity.CategoryId = dto.CategoryId;
            entity.UpdatedAt = DateTime.UtcNow;
            _repo.Update(entity);
            await _uow.SaveChangesAsync();
        }
    }
}
