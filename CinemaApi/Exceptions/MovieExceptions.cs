namespace CinemaApi.Exceptions
{
    public class MovieNotFoundException : AppException
    {
        public MovieNotFoundException(int id)
            : base($"Movie with ID {id} was not found.") { }
    }

    public class MovieUpdateFailedException : AppException
    {
        public MovieUpdateFailedException(int id)
            : base($"Failed to update Movie with ID {id}.") { }
    }

    public class GetAllMoviesFailedException : AppException
    {
        public GetAllMoviesFailedException()
            : base("Failed to retrieve list of Movies.") { }
    }
}