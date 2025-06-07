namespace CinemaApi.Models
{
    public class Director : BaseEntity
    {
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Nationality { get; set; }

        public ICollection<Movie>? Movies { get; set; }
    }
}
