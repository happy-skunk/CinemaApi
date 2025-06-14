using CinemaApi.DTOs.Director;
using CinemaApi.Mapper;
using CinemaApi.Repository.Specific;
using CinemaApi.Logger;
using CinemaApi.Exceptions;

namespace CinemaApi.Services.Director
{
    public class DirectorService : IDirectorService
    {
        private readonly IDirectorRepository _directorRepo;
        private readonly Logging _logger;

        public DirectorService(IDirectorRepository directorRepository, Logging logger)
        {
            _directorRepo = directorRepository;
            _logger = logger;
        }

        public async Task<int> CreateDirectorAsync(DirectorCreateDto dto)
        {
            var director = dto.ToDirector();
            await _directorRepo.Add(director);
            return director.Id;
        }

        public async Task<IEnumerable<DirectorViewDto>> GetAllDirectorsAsync()
        {
            try
            {
                var directors = await _directorRepo.GetAllIncludingAsync();
                return directors.Select(d => d.ToDirectorViewDto());
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString());
                throw new GetAllDirectorsFailedException();
            }
        }

        public async Task<DirectorViewDto> GetDirectorByIdAsync(int id)
        {
            var director = await _directorRepo.GetByIdIncludingAsync(id);
            if (director == null)
                throw new GetAllDirectorsFailedException();
            return director.ToDirectorViewDto();
        }

        public async Task UpdateDirectorAsync(DirectorUpdateDto dto)
        {
            try
            {
                var director = await _directorRepo.GetById(dto.Id);
                if (director == null) return;

                director.FullName = dto.FullName;
                director.BirthDate = dto.BirthDate;
                director.Nationality = dto.Nationality;

                await _directorRepo.Update(director);
            }
            catch (DirectorNotFoundException) { throw; }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString());
                throw new DirectorUpdateFailedException(dto.Id);
            }
        }

        public async Task<DirectorDeleteDto> DeleteDirectorAsync(int id)
        {
            var director = await _directorRepo.GetById(id);
            if (director == null) 
                throw new DirectorNotFoundException(id);

            await _directorRepo.Delete(id);

            return new DirectorDeleteDto { Id = director.Id };
        }
    }
}
