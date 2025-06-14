using CinemaApi.DTOs.Movie;
using CinemaApi.Exceptions;
using CinemaApi.Logger;
using CinemaApi.Mapper;
using CinemaApi.Models;
using CinemaApi.Repository.Specific;
using Humanizer;

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
            try
            {
                var movies = await _movieRepo.GetAllIncludingAsync();
                return movies.Select(MovieMapper.ToMovieViewDto).ToList();
            }
            catch (Exception ex) 
            {
                _logger.Log(ex.ToString());
                throw new GetAllMoviesFailedException();
            }
        }

        public async Task<MovieViewDto> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepo.GetByIdIncludingAsync(id);
            if (movie == null)
                throw new MovieNotFoundException(id);

            return movie.ToMovieViewDto();

        }

        public async Task UpdateMovieAsync(MovieUpdateDto dto)
        {
            try
            {
                var movie = await _movieRepo.GetById(dto.Id);
                if (movie == null)
                    throw new MovieNotFoundException(dto.Id);

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
            catch (MovieNotFoundException) { throw; }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString());
                throw new MovieUpdateFailedException(dto.Id);
            }
        }

        public async Task<MovieDeleteDto> DeleteMovieAsync(int id)
        {
            var movie = await _movieRepo.GetById(id);
            if (movie == null)
                throw new MovieNotFoundException(id);

            await _movieRepo.Delete(id);

            return new MovieDeleteDto { Id = movie.Id };
        }


        public async Task<IEnumerable<MovieViewDto>> GetMoviesByDirectorNameAsync(string directorName)
        {
            try
            {
                var movies = await _movieRepo.GetMoviesByDirectorNameAsync(directorName);
                return movies.Select(MovieMapper.ToMovieViewDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString());
                throw new GetAllMoviesFailedException();
            }
        }

        public async Task<IEnumerable<MovieViewDto>> GetMoviesByGenreNameAsync(string genreName)
        {
            try
            {
                var movies = await _movieRepo.GetMoviesByGenreNameAsync(genreName);
                return movies.Select(MovieMapper.ToMovieViewDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString());
                throw new GetAllMoviesFailedException();
            }
        }

        public async Task<IEnumerable<MovieViewDto>> GetMoviesByRatingRangeAsync(double min, double max)
        {
            try
            {
                var movies = await _movieRepo.GetMoviesByRatingRangeAsync(min, max);
                return movies.Select(MovieMapper.ToMovieViewDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString());
                throw new GetAllMoviesFailedException();
            }
        }

        public async Task<IEnumerable<MovieViewDto>> GetMoviesByActorNameAsync(string actorName)
        {
            try
            {
                var movies = await _movieRepo.GetMoviesByActorNameAsync(actorName);
                return movies.Select(MovieMapper.ToMovieViewDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString());
                throw new GetAllMoviesFailedException();
            }
        }
    }
}
