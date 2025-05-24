using CinemaApi.Models;

namespace CinemaApi.Repository.Specific
{
    public interface IActorRepository : IRepository<Actor>
    {
        Task<IEnumerable<Actor>> GetAllIncludingAsync();
        Task<Actor> GetByIdIncludingAsync(int id);
    }
}
