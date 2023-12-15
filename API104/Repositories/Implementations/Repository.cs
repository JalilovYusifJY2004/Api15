using API104.DAL;
using API104.Entities;
using API104.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API104.Repositories.Implementations
{
    public class Repository:IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }



        public async Task<IQueryable<Category>> GetAllAsync(Expression<Func<Category, bool>>? expression = null, params string[] includes)
        {
            var query = _context.Categories
                .AsQueryable();

            if (expression is not null)
            {
                query = query.Where(expression);
            }

            if (includes is not null)
            {

                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            return query;
        }

        public Task<Category> GetByIdAsync(int id)
        {
            return (_context.Categories
                .FirstOrDefaultAsync(c => c.Id == id));

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

    }
}
