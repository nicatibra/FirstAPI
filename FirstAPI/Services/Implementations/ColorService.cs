
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Services.Implementations
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;

        public ColorService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }



        public async Task<IEnumerable<GetColorDTO>> GetAllColorsAsync(int page, int take)
        {
            IEnumerable<GetColorDTO> colorsDTOs = await _colorRepository
                .GetAll(skip: (page - 1) * take, take: take)
                .Select(c => new GetColorDTO
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();

            return colorsDTOs;
        }


        public async Task<GetColorDetailDTO> GetColorByIdAsync(int id)
        {
            Color color = await _colorRepository.GetByIdAsync(id);

            if (color == null)
                throw new Exception("Not Found");

            GetColorDetailDTO colorDetailDTO = new()
            {
                Id = color.Id,
                Name = color.Name
            };

            return colorDetailDTO;
        }



        public async Task<bool> CreateColorAsync(CreateColorDTO colorDTO)
        {
            if (await _colorRepository.AnyAsync(c => c.Name == colorDTO.Name))
                return false;

            await _colorRepository.AddAsync(new Color
            {
                Name = colorDTO.Name
            });

            await _colorRepository.SaveChangeAsync();
            return true;
        }



        public async Task UpdateColorAsync(int id, UpdateColorDTO colorDTO)
        {
            Color color = await _colorRepository.GetByIdAsync(id);
            if (color == null)
                throw new Exception("Not found");

            if (await _colorRepository.AnyAsync(c => c.Name == colorDTO.Name && c.Id == id))
                throw new Exception("Already exists");

            color.Name = colorDTO.Name;
            _colorRepository.Update(color);
            await _colorRepository.SaveChangeAsync();
        }



        public async Task DeleteColorAsync(int id)
        {
            Color color = await _colorRepository.GetByIdAsync(id);

            if (color == null)
                throw new Exception("Not found");

            _colorRepository.Delete(color);
            await _colorRepository.SaveChangeAsync();
        }
    }
}
