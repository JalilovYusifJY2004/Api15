using API104.DAL;
using API104.Entities;
using API104.Repositories.Interface;
using System.Linq.Expressions;

namespace API104.Repositories.Implementations
{
    public class CategoryRepository :Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context):base(context) 
        {
            context.Categories.OrderBy(c => c.Name); 
        }

    }
}
