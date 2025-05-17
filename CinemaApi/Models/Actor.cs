namespace CinemaApi.Models
{
    public class Actor : BaseEntity
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }

        public ICollection<Actor> Actors { get; set; }
    }
}
