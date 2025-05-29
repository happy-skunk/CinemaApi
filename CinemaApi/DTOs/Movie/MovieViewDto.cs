namespace CinemaApi.DTOs.Movie
{
    public class MovieViewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int DurationMinutes { get; set; }

        public double? Rating { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public List<string> Actors { get; set; }
    }
}
