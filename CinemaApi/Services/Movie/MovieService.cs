using CinemaApi.DTOs.Movie;
using CinemaApi.Logger;
using CinemaApi.Mapper;
using CinemaApi.Models;
using CinemaApi.Repository.Specific;

namespace CinemaApi.Services.Movie
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepo;
        private readonly Logging _logger;

        public MovieService(IMovieRepository movieRepo, Logging logger)
        {
            _movieRepo = movieRepo;
            _logger = logger;
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
            try
            {
                var movie = await _movieRepo.GetById(dto.Id);
                if (movie == null) return;

                movie.Title = dto.Title;
                movie.Description = dto.Description;
                movie.ReleaseDate = dto.ReleaseDate;
                movie.DurationMinutes = dto.DurationMinutes;
                movie.Rating = dto.Rating;
                movie.DirectorId = dto.DirectorId;
                movie.GenreId = dto.GenreId;
                movie.MovieActors = dto.ActorIds?.Select(actorId => new MovieActor
                {
                    ActorId = actorId
                }).ToList();

                await _movieRepo.Update(movie);
            }
            catch (Exception ex) 
            {
                _logger.Log(ex.ToString());
            }
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
            var movies = await _movieRepo.GetMoviesByGenreNameAsync(genreName);
            if (movies == null) return null;

            var result = movies.Select(MovieMapper.ToMovieViewDto).ToList();
            return result;
        }

        public async Task<IEnumerable<MovieViewDto>> GetMoviesByRatingRangeAsync(double min, double max)
        {
            var movies = await _movieRepo.GetMoviesByRatingRangeAsync(min, max);
            if (movies == null) return null;

            var result = movies.Select(MovieMapper.ToMovieViewDto).ToList();
            return result;
        }

        public async Task<IEnumerable<MovieViewDto>> GetMoviesByActorNameAsync(string actorName)
        {
            var movies = await _movieRepo.GetMoviesByActorNameAsync(actorName);
            if (movies == null) return null;

            var result = movies.Select(MovieMapper.ToMovieViewDto).ToList();
            return result;
        }
    }
}
