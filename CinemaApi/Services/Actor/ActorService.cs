using CinemaApi.DTOs.Actor;
using CinemaApi.Models;
using CinemaApi.Repository.Specific;

namespace CinemaApi.Services.Actor
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _ActorRepo;
        public ActorService(IActorRepository ActorRepository)
        {
            _ActorRepo = ActorRepository;
        }

        public async Task<int> CreateActorAsync(ActorCreateDto ActorCreateDto)
        {
            var Actor = new Models.Actor
            {
                FullName = ActorCreateDto.FullName,
            };

            await _ActorRepo.Add(Actor);
            return Actor.Id;
        }

        public async Task<IEnumerable<ActorViewDto>> GetAllActorsAsync()
        {
            var Actor = await _ActorRepo.GetAllIncludingAsync();
            return Actor.Select(Actor => new ActorViewDto
            {
                Id = Actor.Id,
                FullName = Actor.FullName,
                Movies = Actor.MovieActors?.Select(ma => ma.Movie.Title).ToList()
            });

        }

        public async Task<ActorViewDto> GetActorByIdAsync(int id)
        {
            var Actor = await _ActorRepo.GetByIdIncludingAsync(id);
            if (Actor == null) return null;

            return new ActorViewDto
            {
                Id = Actor.Id,
                FullName = Actor.FullName,
                Movies = Actor.MovieActors?.Select(ma => ma.Movie.Title).ToList()
            };
        }

        public async Task UpdateActorAsync(ActorUpdateDto dto)
        {
            var Actor = await _ActorRepo.GetById(dto.Id);
            if (Actor == null) return;

            Actor.FullName = dto.FullName;
            Actor.Id = dto.Id;

            await _ActorRepo.Update(Actor);
        }

        public async Task<ActorDeleteDto> DeleteActorAsync(int id)
        {
            var Actor = await _ActorRepo.GetById(id);
            if (Actor == null) return null;

            await _ActorRepo.Delete(id);

            return new ActorDeleteDto
            {
                Id = Actor.Id,
            };
        }
    }
}
