using CinemaApi.DTOs.Genre;
using CinemaApi.Services.Genre;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _GenreService;

        public GenreController(IGenreService genreService)
        {
            _GenreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _GenreService.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenre(int id)
        {
            var genre = await _GenreService.GetGenreByIdAsync(id);
            if (genre == null) return NotFound();
            return Ok(genre);
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] GenreCreateDto dto)
        {
            var id = await _GenreService.CreateGenreAsync(dto);
            return CreatedAtAction(nameof(GetGenre), new { id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();
            await _GenreService.UpdateGenreAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var deleted = await _GenreService.DeleteGenreAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
