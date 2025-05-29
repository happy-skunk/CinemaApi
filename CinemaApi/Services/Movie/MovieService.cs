using CinemaApi.DTOs.Movie;
using CinemaApi.Mapper;
using CinemaApi.Models;
using CinemaApi.Repository.Specific;

namespace CinemaApi.Services.Movie
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepo;

        public MovieService(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public async Task<int> CreateMovieAsync(MovieCreateDto dto)
        {
            var movie = dto.ToMovie();
            await _movieRepo.Add(movie);
            return movie.Id;
        }

        public async Task<IEnumerable<MovieViewDto>> GetAllMoviesAsync()
        {
            var movies = await _movieRepo.GetAllIncludingAsync();
            return movies.Select(MovieMapper.ToMovieViewDto).ToList();
        }

        public async Task<MovieViewDto> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepo.GetByIdIncludingAsync(id);
            return movie?.ToMovieViewDto();
        }

        public async Task UpdateMovieAsync(MovieUpdateDto dto)
        {
            var movie = await _movieRepo.GetById(dto.Id);
            if (movie == null) return;

            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.ReleaseDate = dto.ReleaseDate;
            movie.DurationMinutes = dto.DurationMinutes;
            movie.DirectorId = dto.DirectorId;
            movie.GenreId = dto.GenreId;

            await _movieRepo.Update(movie);
        }

        public async Task<MovieDeleteDto> DeleteMovieAsync(int id)
        {
            var movie = await _movieRepo.GetById(id);
            if (movie == null) return null;

            await _movieRepo.Delete(id);

            return new MovieDeleteDto
            {
                Id = movie.Id,
            };
        }


        public async Task<IEnumerable<MovieViewDto>> GetMoviesByDirectorNameAsync(string directorName)
        {
            var movies = await _movieRepo.GetMoviesByDirectorNameAsync(directorName);
            if (movies == null) return null;

            var result = movies.Select(MovieMapper.ToMovieViewDto).ToList();
            return result;
        }

        public async Task<IEnumerable<MovieViewDto>> GetMoviesByGenreNameAsync(string genreName)
        {
            var movies = await _movieRepo.GetMoviesByDirectorNameAsync(genreName);
            if (movies == null) return null;

            var result = movies.Select(MovieMapper.ToMovieViewDto).ToList();
            return result;
        }

        public async Task<IEnumerable<MovieViewDto>> GetMoviesByRatingRangeAsync(double min, double max)
        {
            var movies = await _movieRepo.GetMoviesByDirectorNameAsync(Double);
            if (movies == null) return null;

            var result = movies.Select(MovieMapper.ToMovieViewDto).ToList();
            return result;
        }

        public async Task<IEnumerable<MovieViewDto>> GetMoviesByActorNameAsync(string actorName)
        {
            var movies = await _movieRepo.GetMoviesByDirectorNameAsync(actorName);
            if (movies == null) return null;

            var result = movies.Select(MovieMapper.ToMovieViewDto).ToList();
            return result;
        }
    }
}
