using CinemaApi.Models;

namespace CinemaApi.Repository.Specific
{
    public interface IDirectorRepository : IRepository<Director>
    {
        Task<IEnumerable<Director>> GetAllIncludingAsync();
        Task<Director> GetByIdIncludingAsync(int id);
    }
}
