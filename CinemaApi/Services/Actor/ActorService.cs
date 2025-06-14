using CinemaApi.DTOs.Actor;
using CinemaApi.Models;
using CinemaApi.Repository.Specific;
using CinemaApi.Logger;
using CinemaApi.Exceptions;
using CinemaApi.Mapper;

namespace CinemaApi.Services.Actor
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _ActorRepo;
        private readonly Logging _logger;

        public ActorService(IActorRepository ActorRepository, Logging logger)
        {
            _ActorRepo = ActorRepository;
            _logger = logger;
        }

        public async Task<int> CreateActorAsync(ActorCreateDto dto)
        {
            var Actor = dto.ToActor();
            await _ActorRepo.Add(Actor);
            return Actor.Id;
        }

        public async Task<IEnumerable<ActorViewDto>> GetAllActorsAsync()
        {
            try
            {
                var Actor = await _ActorRepo.GetAllIncludingAsync();
                return Actor.Select(a => a.ToActorViewDto());
            }
            catch (Exception ex) 
            {
                _logger.Log(ex.ToString());
                throw new GetAllActorsFailedException();
            }
        }

        public async Task<ActorViewDto> GetActorByIdAsync(int id)
        {
            var actor = await _ActorRepo.GetByIdIncludingAsync(id);
            if (actor == null)
                throw new ActorNotFoundException(id);

            return actor.ToActorViewDto();
        }

        public async Task UpdateActorAsync(ActorUpdateDto dto)
        {
            try
            {
                var actor = await _ActorRepo.GetById(dto.Id);
                if (actor == null) return;

                actor.FullName = dto.FullName;
                actor.BirthDate = dto.BirthDate;
                actor.Nationality = dto.Nationality;

                await _ActorRepo.Update(actor);
            }
            catch (ActorNotFoundException) { throw; }
            catch (Exception ex) 
            {
                _logger.Log(ex.ToString());
                throw new ActorUpdateFailedException(dto.Id);
            }
        }

        public async Task<ActorDeleteDto> DeleteActorAsync(int id)
        {
            var Actor = await _ActorRepo.GetById(id);
            if (Actor == null)
                throw new ActorNotFoundException(id);

            await _ActorRepo.Delete(id);

            return new ActorDeleteDto { Id = Actor.Id };
        }
    }
}
