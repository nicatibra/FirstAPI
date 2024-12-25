using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<GetCategoryDTO>> GetAllAsync(int page, int take)
        {
            IEnumerable<GetCategoryDTO> categoriesDTOs = await _categoryRepository
                .GetAll(skip: (page - 1) * take, take: take)
                .Select(c => new GetCategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductCount = c.Products.Count,
                }).ToListAsync();

            return categoriesDTOs;
        }

        public async Task<GetCategoryDetailDTO> GetByIdAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) { throw new Exception("Not found"); }

            GetCategoryDetailDTO categoryDTO = new()
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products.Select()
            };

            return categoryDTO;
        }
    }
}
