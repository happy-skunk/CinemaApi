using CinemaApi.DTOs.Director;

namespace CinemaApi.Services.Director
{
    public interface IDirectorService
    {
        Task<int> CreateDirectorAsync(DirectorCreateDto DirectorCreateDto);
        Task<IEnumerable<DirectorViewDto>> GetAllDirectorsAsync();
        Task<DirectorViewDto> GetDirectorByIdAsync(int id);
        Task UpdateDirectorAsync(DirectorUpdateDto dto);
        Task<DirectorDeleteDto> DeleteDirectorAsync(int id);
    }
}
