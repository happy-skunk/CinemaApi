using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Repository.Specific
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllIncludingAsync()
        {
            return await _context.Genres
                .Include(g => g.Movies)
                .ToListAsync();
        }

        public async Task<Genre> GetByIdIncludingAsync(int id)
        {
            return await _context.Genres
                .Include(g => g.Movies)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
