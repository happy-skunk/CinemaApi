using CinemaApi.Models;

namespace CinemaApi.Repository.Specific
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<IEnumerable<Genre>> GetAllIncludingAsync();
        Task<Genre> GetByIdIncludingAsync(int id);
    }
}
