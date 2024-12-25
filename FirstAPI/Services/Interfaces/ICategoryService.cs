namespace FirstAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDTO>> GetAllCategoriesAsync(int page, int take);

        Task<GetCategoryDetailDTO> GetCategoryByIdAsync(int id);

        Task<bool> CreateCategoryAsync(CreateCategoryDTO categoryDTO);

        Task UpdateCategoryAsync(int id, UpdateCategoryDTO categoryDTO);

        Task DeleteCategoryAsync(int id);
    }
}
