using CinemaApi.DTOs.Director;
using CinemaApi.Models;

namespace CinemaApi.Mapper
{
    public static class DirectorMapper
    {
        public static Director ToDirector(this DirectorCreateDto dto)
        {
            return new Director
            {
                FullName = dto.FullName,
                BirthDate = dto.BirthDate,
                Nationality = dto.Nationality,
            };
        }

        public static Director ToDirector(this DirectorUpdateDto dto)
        {
            return new Director
            {
                Id = dto.Id,
                FullName = dto.FullName,
                BirthDate = dto.BirthDate,
                Nationality = dto.Nationality,
            };
        }

        public static DirectorViewDto ToDirectorViewDto(this Director director)
        {
            return new DirectorViewDto
            {
                Id = director.Id,
                FullName = director.FullName,
                BirthDate = director.BirthDate,
                Nationality = director.Nationality,
                Movies = director.Movies?.Select(m => m.Title).ToList()
            };
        }

    }
}
