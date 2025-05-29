using CinemaApi.DTOs.Director;
using CinemaApi.Mapper;
using CinemaApi.Repository.Specific;

namespace CinemaApi.Services.Director
{
    public class DirectorService : IDirectorService
    {
        private readonly IDirectorRepository _directorRepo;

        public DirectorService(IDirectorRepository directorRepository)
        {
            _directorRepo = directorRepository;
        }

        public async Task<int> CreateDirectorAsync(DirectorCreateDto dto)
        {
            var director = dto.ToDirector();
            await _directorRepo.Add(director);
            return director.Id;
        }

        public async Task<IEnumerable<DirectorViewDto>> GetAllDirectorsAsync()
        {
            var directors = await _directorRepo.GetAllIncludingAsync();
            return directors.Select(d => d.ToDirectorViewDto());
        }

        public async Task<DirectorViewDto> GetDirectorByIdAsync(int id)
        {
            var director = await _directorRepo.GetByIdIncludingAsync(id);
            return director?.ToDirectorViewDto();
        }

        public async Task UpdateDirectorAsync(DirectorUpdateDto dto)
        {
            var director = await _directorRepo.GetById(dto.Id);
            if (director == null) return;

            director.FullName = dto.FullName;
            director.BirthDate = dto.BirthDate;
            director.Nationality = dto.Nationality;

            await _directorRepo.Update(director);
        }

        public async Task<DirectorDeleteDto> DeleteDirectorAsync(int id)
        {
            var director = await _directorRepo.GetById(id);
            if (director == null) return null;

            await _directorRepo.Delete(id);

            return new DirectorDeleteDto { Id = director.Id };
        }
    }
}
