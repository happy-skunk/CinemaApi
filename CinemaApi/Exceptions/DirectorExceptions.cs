namespace CinemaApi.Exceptions
{
    public class DirectorNotFoundException : AppException
    {
        public DirectorNotFoundException(int id)
            : base($"Director with ID {id} was not found.") { }
    }

    public class DirectorUpdateFailedException : AppException
    {
        public DirectorUpdateFailedException(int id)
            : base($"Failed to update Director with ID {id}.") { }
    }

    public class GetAllDirectorsFailedException : AppException
    {
        public GetAllDirectorsFailedException()
            : base("Failed to retrieve list of Directors.") { }
    }
}
