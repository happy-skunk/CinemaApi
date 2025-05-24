namespace CinemaApi.DTOs.Director
{
    public class DirectorViewDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public List<string> Movies { get; set; }
    }
}
