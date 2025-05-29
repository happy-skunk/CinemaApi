using CinemaApi.DTOs.Actor;
using CinemaApi.Models;

namespace CinemaApi.Mapper
{
    public static class ActorMapper
    {
        public static Actor ToActor(this ActorCreateDto dto)
        {
            return new Actor
            {
                FullName = dto.FullName
            };
        }

        public static ActorViewDto ToActorViewDto(this Actor actor)
        {
            return new ActorViewDto
            {
                Id = actor.Id,
                FullName = actor.FullName,
                Movies = actor.MovieActors?.Select(ma => ma.Movie.Title).ToList()
            };
        }
    }
}
