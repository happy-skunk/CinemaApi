namespace CinemaApi.DTOs.Director
{
    public class DirectorCreateDto
    {
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Nationality { get; set; }
    }
}
