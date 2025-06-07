using CinemaApi.DTOs.Movie;
using CinemaApi.Models;

namespace CinemaApi.Mapper
{
    public static class MovieMapper
    {
        public static Movie ToMovie(this MovieCreateDto dto)
        {
            return new Movie
            {
                Title = dto.Title,
                Description = dto.Description,
                ReleaseDate = dto.ReleaseDate,
                DurationMinutes = dto.DurationMinutes,
                Rating = dto?.Rating,
                GenreId = dto.GenreId,
                DirectorId = dto.DirectorId,
                MovieActors = dto.ActorIds?.Select(actorId => new MovieActor
                {
                    ActorId = actorId
                }).ToList()
            };
        }

        public static Movie ToMovie(this MovieUpdateDto dto)
        {
            return new Movie
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                ReleaseDate = dto.ReleaseDate,
                DurationMinutes = dto.DurationMinutes,
                Rating = dto?.Rating,
                GenreId = dto.GenreId,
                DirectorId = dto.DirectorId,
                MovieActors = dto.ActorIds?.Select(actorId => new MovieActor
                {
                    ActorId = actorId
                }).ToList()
            };
        }

        public static MovieViewDto ToMovieViewDto(this Movie movie)
        {
            return new MovieViewDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                DurationMinutes = movie.DurationMinutes,
                Rating = movie?.Rating,
                Genre = movie.Genre?.Name,
                Director = movie.Director?.FullName,
                Actors = movie.MovieActors?.Select(ma => ma.Actor?.FullName).ToList(),
            };
        }
    }
}
