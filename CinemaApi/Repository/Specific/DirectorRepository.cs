using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Repository.Specific
{
    public class DirectorRepository : Repository<Director>, IDirectorRepository
    {
        private readonly AppDbContext _context;

        public DirectorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Director>> GetAllIncludingAsync()
        {
            return await _context.Directors
                .Include(d => d.Movies)
                .ToListAsync();
        }

        public async Task<Director> GetByIdIncludingAsync(int id)
        {
            return await _context.Directors
                .Include(d => d.Movies)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
