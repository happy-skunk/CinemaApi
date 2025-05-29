using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaApi.Models
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int DurationMinutes { get; set; }

        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        [ForeignKey("Director")]
        public int DirectorId { get; set; }
        public Director Director { get; set; }

        public double? Rating { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
