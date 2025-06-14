namespace CinemaApi.Exceptions
{
    public class GenreNotFoundException : AppException
    {
        public GenreNotFoundException(int id)
            : base($"Genre with ID {id} was not found.") { }
    }

    public class GenreUpdateFailedException : AppException
    {
        public GenreUpdateFailedException(int id)
            : base($"Failed to update Genre with ID {id}.") { }
    }

    public class GetAllGenresFailedException : AppException
    {
        public GetAllGenresFailedException()
            : base("Failed to retrieve list of Genres.") { }
    }
}
