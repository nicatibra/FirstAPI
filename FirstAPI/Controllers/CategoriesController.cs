using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryRepository repository, ICategoryService service)
        {
            _repository = repository;
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get(int page, int take = 3)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            Category category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return await _service.GetByIdAsync(id)
            return StatusCode(StatusCodes.Status200OK, category);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDTO categoryDTO)
        {
            Category category = new()
            {
                Name = categoryDTO.Name,
            };

            await _repository.AddAsync(category);
            await _repository.SaveChangeAsync();

            return StatusCode(StatusCodes.Status201Created, category);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            Category category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _repository.Delete(category);
            await _repository.SaveChangeAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            Category category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = name;

            _repository.Update(category);
            await _repository.SaveChangeAsync();

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
