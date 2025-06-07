namespace CinemaApi.DTOs.Actor
{
    public class ActorViewDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Nationality { get; set; }
        public List<string>? Movies { get; set; }
    }
}
