namespace CinemaApi.Logger
{
    public class Logging : ILogging
    {
        private readonly string _logFilePath;

        public Logging() 
        {
            var logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(logDir);

            _logFilePath = Path.Combine(logDir, $"log_{DateTime.UtcNow:yyyyMMdd}.txt");
        }

        public void Log(string message) 
        {
            var logEntry = $"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
            using (StreamWriter sw = new StreamWriter(_logFilePath, append: true)) 
            {
                sw.WriteLine(message);
            }
        }
    }
}
