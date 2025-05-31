using Microsoft.Extensions.Caching.Memory;

namespace CinemaApi.Cache
{
    public class CacheService : ICacheService
    {
        private class CachedItem<T> 
        {
            public T Value { get; set; }
            public DateTime expiration { get; set; }
        }

        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key) 
        {
            if (_cache.TryGetValue(key, out CachedItem<T> cachedItem)) 
            {
                if (DateTime.Now < cachedItem.expiration) 
                {
                    return cachedItem.Value;
                }
                else _cache.Remove(key);
            }
            return default;
        }

        public void Set<T>(string key, T value, TimeSpan duration) 
        {
            var cachedItem = new CachedItem<T>
            {
                Value = value,
                expiration = DateTime.Now.Add(duration)
            };
            _cache.Set(key, cachedItem, duration);
        }

        public void Remove(string key) 
        {
            _cache.Remove(key);
        }
    }
}
