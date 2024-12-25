using FirstAPI.DTOs.Product;
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


        public async Task<IEnumerable<GetCategoryDTO>> GetAllCategoriesAsync(int page, int take)
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

        public async Task<GetCategoryDetailDTO> GetCategoryByIdAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id, nameof(Category.Products));

            if (category == null)
                throw new Exception("Not found");

            GetCategoryDetailDTO categoryDetailDTO = new()
            {
                Id = category.Id,
                Name = category.Name,
                ProductDTOs = category.Products
                .Select(p => new GetProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                }).ToList()
            };

            return categoryDetailDTO;
        }

        public async Task<bool> CreateCategoryAsync(CreateCategoryDTO categoryDTO)
        {
            if (await _categoryRepository.AnyAsync(c => c.Name == categoryDTO.Name))
                return false;

            await _categoryRepository.AddAsync(new Category
            {
                Name = categoryDTO.Name
            });

            await _categoryRepository.SaveChangeAsync();

            return true;
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategoryDTO categoryDTO)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new Exception("Not found");

            if (await _categoryRepository.AnyAsync(c => c.Name == categoryDTO.Name && c.Id == id))
                throw new Exception("Already exists");

            category.Name = categoryDTO.Name;
            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangeAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                throw new Exception("Not found");

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangeAsync();
        }
    }
}
