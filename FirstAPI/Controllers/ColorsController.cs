using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _service;

        public ColorsController(IColorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page, int take = 3)
        {
            return Ok(await _service.GetAllColorsAsync(page, take));
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest();

            var colorDetailDTO = await _service.GetColorByIdAsync(id);

            if (colorDetailDTO == null)
                return NotFound();

            return StatusCode(StatusCodes.Status200OK, colorDetailDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateColorDTO colorDTO)
        {
            if (!await _service.CreateColorAsync(colorDTO))
                return BadRequest();

            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateColorDTO colorDTO)
        {
            if (id < 1)
                return BadRequest();

            await _service.UpdateColorAsync(id, colorDTO);

            return StatusCode(StatusCodes.Status204NoContent);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            await _service.DeleteColorAsync(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
