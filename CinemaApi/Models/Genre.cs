namespace CinemaApi.Models
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Movie>? Movies { get; set; }
    }
}
