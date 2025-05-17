namespace CinemaApi.Models
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int DurationMinutes { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public int DirectorId { get; set; }
        public Director Director { get; set; }

        public string ImdbRating { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
