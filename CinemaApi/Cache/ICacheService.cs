namespace CinemaApi.Cache
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Set<T>(string Key, T value, TimeSpan duration);
        void Remove(string key);
    }
}
