using CinemaApi.DTOs.Movie;
using CinemaApi.Services.Movie;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] MovieCreateDto dto)
        {
            var id = await _movieService.CreateMovieAsync(dto);
            return CreatedAtAction(nameof(GetMovie), new { id }, dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();
            await _movieService.UpdateMovieAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var deleted = await _movieService.DeleteMovieAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }

        [HttpGet("by-actor/{name}")]
        public async Task<IActionResult> GetMoviesByActor(string name)
        {
            var movies = await _movieService.GetMoviesByActorNameAsync(name);
            return Ok(movies);
        }

        [HttpGet("by-director/{name}")]
        public async Task<IActionResult> GetMoviesByDirector(string name)
        {
            var movies = await _movieService.GetMoviesByDirectorNameAsync(name);
            return Ok(movies);
        }

        [HttpGet("by-genre/{name}")]
        public async Task<IActionResult> GetMoviesByGenre(string name)
        {
            var movies = await _movieService.GetMoviesByGenreNameAsync(name);
            return Ok(movies);
        }

        [HttpGet("by-rating/{min}/{max}")]
        public async Task<IActionResult> GetMoviesByRating(double min, double max)
        {
            var movies = await _movieService.GetMoviesByRatingRangeAsync(min, max);
            return Ok(movies);
        }
    }
}
