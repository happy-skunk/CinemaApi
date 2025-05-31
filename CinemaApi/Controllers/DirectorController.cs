using CinemaApi.DTOs.Director;
using CinemaApi.Services.Director;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _DirectorService;

        public DirectorController(IDirectorService directorService)
        {
            _DirectorService = directorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDirectors()
        {
            var directors = await _DirectorService.GetAllDirectorsAsync();
            return Ok(directors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirector(int id)
        {
            var director = await _DirectorService.GetDirectorByIdAsync(id);
            if (director == null) return NotFound();
            return Ok(director);
        }

        [HttpPost]
        public async Task<IActionResult> AddDirector([FromBody] DirectorCreateDto dto)
        {
            var id = await _DirectorService.CreateDirectorAsync(dto);
            return CreatedAtAction(nameof(GetDirector), new { id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDirector(int id, [FromBody] DirectorUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();
            await _DirectorService.UpdateDirectorAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var deleted = await _DirectorService.DeleteDirectorAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
