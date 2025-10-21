using GPro_BootCamp2_7_10_Infrastructure.Persistence;
using GPro_BootCamp2_7_10_Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Infrastructure.Repositories
{
    public class MainRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public MainRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
          await  _context.Set<T>().AddAsync(entity);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T>Query() => _context.Set<T>().AsQueryable();


        public void Remove(T entity)
        {
           _context.Set<T>().Remove(entity);
        }

        public void Update(T entity) => _context.Set<T>().Update(entity);

    }
}
