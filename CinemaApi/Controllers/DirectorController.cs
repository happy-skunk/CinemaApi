using CinemaApi.DTOs.Director;
using CinemaApi.Services.Director;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _directorService;

        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        [HttpPost]
        public async Task<IActionResult> AddDirector([FromBody] DirectorCreateDto dto)
        {
            var id = await _directorService.CreateDirectorAsync(dto);
            return CreatedAtAction(nameof(GetDirector), new { id }, dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetDirectors()
        {
            var directors = await _directorService.GetAllDirectorsAsync();
            return Ok(directors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirector(int id)
        {
            var director = await _directorService.GetDirectorByIdAsync(id);
            if (director == null) return NotFound();
            return Ok(director);
        }

        [HttpPut)]
        public async Task<IActionResult> UpdateDirector([FromBody] DirectorUpdateDto dto)
        {
            await _directorService.UpdateDirectorAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var deleted = await _directorService.DeleteDirectorAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
