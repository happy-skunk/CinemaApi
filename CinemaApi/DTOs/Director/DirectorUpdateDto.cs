namespace CinemaApi.DTOs.Director
{
    public class DirectorUpdateDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
    }
}
