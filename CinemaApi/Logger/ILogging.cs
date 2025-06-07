using Newtonsoft.Json.Bson;

namespace CinemaApi.Logger
{
    public interface ILogging
    {
        public void Log(string message);
    }
}
