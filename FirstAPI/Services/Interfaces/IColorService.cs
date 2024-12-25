namespace FirstAPI.Services.Interfaces
{
    public interface IColorService
    {
        Task<IEnumerable<GetColorDTO>> GetAllColorsAsync(int page, int take);

        Task<GetColorDetailDTO> GetColorByIdAsync(int id);

        Task<bool> CreateColorAsync(CreateColorDTO colorDTO);

        Task UpdateColorAsync(int id, UpdateColorDTO colorDTO);

        Task DeleteColorAsync(int id);
    }
}
