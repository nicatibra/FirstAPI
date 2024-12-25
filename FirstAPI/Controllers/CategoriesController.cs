using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get(int page, int take = 3)
        {
            return Ok(await _service.GetAllCategoriesAsync(page, take));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest();

            var categoryDetailDTO = await _service.GetCategoryByIdAsync(id);

            if (categoryDetailDTO == null)
                return NotFound();

            return StatusCode(StatusCodes.Status200OK, categoryDetailDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDTO categoryDTO)
        {
            if (!await _service.CreateCategoryAsync(categoryDTO))
                return BadRequest();

            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCategoryDTO categoryDTO)
        {
            if (id < 1)
                return BadRequest();

            await _service.UpdateCategoryAsync(id, categoryDTO);

            return StatusCode(StatusCodes.Status204NoContent);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _service.DeleteCategoryAsync(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
