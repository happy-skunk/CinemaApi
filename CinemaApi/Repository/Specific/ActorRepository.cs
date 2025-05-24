using CinemaApi.Data;
using CinemaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Repository.Specific
{
    public class ActorRepository : Repository<Actor>, IActorRepository
    {
        private readonly AppDbContext _context;

        public ActorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Actor>> GetAllIncludingAsync()
        {
            return await _context.Actors
                .Include(a => a.MovieActors)
                    .ThenInclude(ma => ma.Movie)
                .ToListAsync();
        }

        public async Task<Actor> GetByIdIncludingAsync(int id)
        {
            return await _context.Actors
                .Include(a => a.MovieActors)
                    .ThenInclude(ma => ma.Movie)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
