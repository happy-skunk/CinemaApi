using CinemaApi.DTOs.Movie;
using CinemaApi.Models;

namespace CinemaApi.Services.Movie
{
    public interface IMovieService
    {
        Task<int> CreateMovieAsync(MovieCreateDto dto);
        Task<IEnumerable<MovieViewDto>> GetAllMoviesAsync();
        Task<MovieViewDto> GetMovieByIdAsync(int id);
        Task UpdateMovieAsync(MovieUpdateDto dto);
        Task<MovieDeleteDto> DeleteMovieAsync(int id);
        Task<IEnumerable<MovieViewDto>> GetMoviesByDirectorNameAsync(string directorName);
        Task<IEnumerable<MovieViewDto>> GetMoviesByGenreNameAsync(string genreName);
        Task<IEnumerable<MovieViewDto>> GetMoviesByRatingRangeAsync(double min, double max);
        Task<IEnumerable<MovieViewDto>> GetMoviesByActorNameAsync(string actorName);
    }
}
