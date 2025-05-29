using CinemaApi.DTOs.Actor;

namespace CinemaApi.Services.Actor
{
    public interface IActorService
    {
        Task<int> CreateActorAsync(ActorCreateDto actorCreateDto);
        Task<IEnumerable<ActorViewDto>> GetAllActorsAsync();
        Task<ActorViewDto> GetActorByIdAsync(int id);
        Task UpdateActorAsync(ActorUpdateDto dto);
        Task<ActorDeleteDto> DeleteActorAsync(int id);
    }
}
