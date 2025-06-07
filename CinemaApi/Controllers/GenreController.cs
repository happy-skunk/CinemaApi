using CinemaApi.DTOs.Genre;
using CinemaApi.Services.Genre;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] GenreCreateDto dto)
        {
            var id = await _genreService.CreateGenreAsync(dto);
            return CreatedAtAction(nameof(GetGenre), new { id }, dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenre(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null) return NotFound();
            return Ok(genre);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGenre([FromBody] GenreUpdateDto dto)
        {
            await _genreService.UpdateGenreAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var deleted = await _genreService.DeleteGenreAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
