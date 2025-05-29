using CinemaApi.DTOs.Genre;
using CinemaApi.Models;

namespace CinemaApi.Mapper
{
    public static class GenreMapper
    {
        public static Genre ToGenre(this GenreCreateDto dto)
        {
            return new Genre
            {
                Name = dto.Name
            };
        }

        public static Genre ToGenre(this GenreUpdateDto dto)
        {
            return new Genre
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public static GenreViewDto ToGenreViewDto(this Genre genre)
        {
            return new GenreViewDto
            {
                Id = genre.Id,
                Name = genre.Name,
                Movies = genre.Movies?.Select(m => m.Title).ToList()
            };
        }
    }
}
