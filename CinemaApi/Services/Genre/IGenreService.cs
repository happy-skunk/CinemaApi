using CinemaApi.DTOs.Genre;

namespace CinemaApi.Services.Genre
{
    public interface IGenreService
    {
        Task<int> CreateGenreAsync(GenreCreateDto GenreCreateDto);
        Task<IEnumerable<GenreViewDto>> GetAllGenresAsync();
        Task<GenreViewDto> GetGenreByIdAsync(int id);
        Task UpdateGenreAsync(GenreUpdateDto dto);
        Task<GenreDeleteDto> DeleteGenreAsync(int id);
    }
}
