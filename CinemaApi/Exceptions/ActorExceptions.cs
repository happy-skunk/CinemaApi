namespace CinemaApi.Exceptions
{
    public class ActorNotFoundException : AppException
    {
        public ActorNotFoundException(int id)
            : base($"Actor with ID {id} was not found.") { }
    }

    public class ActorUpdateFailedException : AppException
    {
        public ActorUpdateFailedException(int id)
            : base($"Failed to update Actor with ID {id}.") { }
    }

    public class GetAllActorsFailedException : AppException
    {
        public GetAllActorsFailedException()
            : base("Failed to retrieve list of Actors.") { }
    }
}
