using CinemaApi.Cache;
using CinemaApi.DTOs.Movie;
using CinemaApi.Services.Movie;

namespace CinemaApi.Decorator
{
    public class MovieCacheDecorator : IMovieService
    {
        private readonly IMovieService _inner;
        private readonly ICacheService _cache;

        public MovieCacheDecorator(IMovieService inner, ICacheService cache)
        {
            _inner = inner;
            _cache = cache;
        }

        public async Task<IEnumerable<MovieViewDto>> GetAllMoviesAsync()
        {
            var cacheKey = "movies_all";
            var cached = _cache.Get<IEnumerable<MovieViewDto>>(cacheKey);
            if (cached != null) return cached;

            var result = await _inner.GetAllMoviesAsync();
            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
            return result;
        }

        public async Task<MovieViewDto> GetMovieByIdAsync(int id)
        {
            var cacheKey = $"movie_{id}";
            var cached = _cache.Get<MovieViewDto>(cacheKey);
            if (cached != null) return cached;

            var result = await _inner.GetMovieByIdAsync(id);
            if (result != null)
            {
                _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
            }
            return result;
        }

        public Task<int> CreateMovieAsync(MovieCreateDto dto)
        {
            _cache.Remove("movies_all");
            return _inner.CreateMovieAsync(dto);
        }

        public async Task UpdateMovieAsync(MovieUpdateDto dto)
        {
            await _inner.UpdateMovieAsync(dto);
            _cache.Remove($"movie_{dto.Id}");
            _cache.Remove("movies_all");
        }

        public async Task<MovieDeleteDto> DeleteMovieAsync(int id)
        {
            var result = await _inner.DeleteMovieAsync(id);
            _cache.Remove($"movie_{id}");
            _cache.Remove("movies_all");
            return result;
        }

        public Task<IEnumerable<MovieViewDto>> GetMoviesByActorNameAsync(string name)
        {
            var key = $"movies_actor_{name.ToLower()}";
            var cached = _cache.Get<IEnumerable<MovieViewDto>>(key);
            if (cached != null) return Task.FromResult(cached);

            return CacheAndReturn(() => _inner.GetMoviesByActorNameAsync(name), key);
        }

        public Task<IEnumerable<MovieViewDto>> GetMoviesByDirectorNameAsync(string name)
        {
            var key = $"movies_director_{name.ToLower()}";
            var cached = _cache.Get<IEnumerable<MovieViewDto>>(key);
            if (cached != null) return Task.FromResult(cached);

            return CacheAndReturn(() => _inner.GetMoviesByDirectorNameAsync(name), key);
        }

        public Task<IEnumerable<MovieViewDto>> GetMoviesByGenreNameAsync(string name)
        {
            var key = $"movies_genre_{name.ToLower()}";
            var cached = _cache.Get<IEnumerable<MovieViewDto>>(key);
            if (cached != null) return Task.FromResult(cached);

            return CacheAndReturn(() => _inner.GetMoviesByGenreNameAsync(name), key);
        }

        public Task<IEnumerable<MovieViewDto>> GetMoviesByRatingRangeAsync(double min, double max)
        {
            var key = $"movies_rating_{min}_{max}";
            var cached = _cache.Get<IEnumerable<MovieViewDto>>(key);
            if (cached != null) return Task.FromResult(cached);

            return CacheAndReturn(() => _inner.GetMoviesByRatingRangeAsync(min, max), key);
        }

        private async Task<IEnumerable<MovieViewDto>> CacheAndReturn(Func<Task<IEnumerable<MovieViewDto>>> func, string key)
        {
            var result = await func();
            _cache.Set(key, result, TimeSpan.FromMinutes(5));
            return result;
        }
    }
}
