namespace CinemaApi.DTOs.Genre
{
    public class GenreViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Movies { get; set; }
    }
}
