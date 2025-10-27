using GPro_BootCamp2_7_10_Infrastructure.Persistence;
using GPro_BootCamp2_7_10_Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Infrastructure.Repositories
{
    public class MainRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _ctx;
        public MainRepository(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<T?> GetByIdAsync(int id) => await _ctx.Set<T>().FindAsync(id);
        public IQueryable<T> Query() => _ctx.Set<T>().AsQueryable();
        public IQueryable<T> QueryUnfiltered() => _ctx.Set<T>().IgnoreQueryFilters();
        public async Task AddAsync(T entity) => await _ctx.Set<T>().AddAsync(entity);
        public void Update(T entity) => _ctx.Set<T>().Update(entity);
        public void Remove(T entity) => _ctx.Set<T>().Remove(entity);
    }
}
