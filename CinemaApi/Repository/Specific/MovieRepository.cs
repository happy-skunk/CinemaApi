using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Repository.Specific
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllIncludingAsync()
        {
            return await _context.Movies
                .Include(m => m.Director)
                .Include(m => m.Genre)
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .ToListAsync();
        }

        public async Task<Movie> GetByIdIncludingAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Director)
                .Include(m => m.Genre)
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByDirectorNameAsync(string directorName)
        {
            return await _context.Movies
                .Where(m => m.Director != null && m.Director.FullName == directorName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreNameAsync(string genreName)
        {
            return await _context.Movies
                .Where(m => m.Genre != null && m.Genre.Name == genreName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesByActorNameAsync(string actorName)
        {
            return await _context.Movies
                .Where(m => m.MovieActors.Any(ma => ma.Actor != null && ma.Actor.FullName == actorName))
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .ToListAsync();
        }


        public async Task<IEnumerable<Movie>> GetMoviesByRatingRangeAsync(double min, double max)
        {
            return await _context.Movies
                .Where(m => m.ImdbRating >= min && m.ImdbRating <= max)
                .ToListAsync();
        }
    }
}
