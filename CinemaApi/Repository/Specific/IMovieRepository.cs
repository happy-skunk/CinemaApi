using CinemaApi.Models;

namespace CinemaApi.Repository.Specific
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetAllIncludingAsync();
        Task<Movie> GetByIdIncludingAsync(int id);
        Task<IEnumerable<Movie>> GetMoviesByDirectorNameAsync(string directorName);
        Task<IEnumerable<Movie>> GetMoviesByGenreNameAsync(string genreName);
        Task<IEnumerable<Movie>> GetMoviesByRatingRangeAsync(double min, double max);
        Task<IEnumerable<Movie>> GetMoviesByActorNameAsync(string actorName);
    }
}