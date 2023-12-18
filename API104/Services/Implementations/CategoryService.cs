using API104.Dtos;
using API104.Entities;
using API104.Repositories.Interface;
using API104.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API104.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
           _repository = repository;
        }

    

        public async Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllAsync(skip:(page-1)*take,take:take,isTracking:false).ToListAsync();
            ICollection<GetCategoryDto> categoryDtos=new List<GetCategoryDto>();
foreach (var category in categories)
            {
                categoryDtos.Add(new GetCategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }
return categoryDtos;
        }

        public async Task<GetCategoryDto> GetAsync(int id)
        {
        Category category=   await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not");
            return new GetCategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };

        }
        public async Task CreateAsync(GetCategoryDto categoryDto)
        {
           Category category = new Category
           {
             
               Name = categoryDto.Name

           };
            _repository.AddAsync(category);
        }

        public async Task UpdateAsync(int id, UpdateCategoryDto categoryDto)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not found");
            category.Name = categoryDto.Name;
            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Not found");
            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }
    }
}
