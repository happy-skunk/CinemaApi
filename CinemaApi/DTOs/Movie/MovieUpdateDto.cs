namespace CinemaApi.DTOs.Movie
{
    public class MovieUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int DurationMinutes { get; set; }
        public int DirectorId { get; set; }
        public int GenreId { get; set; }
        public List<int> ActorIds { get; set; }
    }
}
