namespace CinemaApi.DTOs.Actor
{
    public class ActorCreateDto
    {
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Nationality { get; set; }
    }
}
