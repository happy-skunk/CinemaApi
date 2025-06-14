using CinemaApi.DTOs.Genre;
using CinemaApi.Mapper;
using CinemaApi.Repository.Specific;
using CinemaApi.Logger;
using CinemaApi.Exceptions;

namespace CinemaApi.Services.Genre
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _GenreRepo;
        private readonly Logging _logger;

        public GenreService(IGenreRepository genreRepository, Logging logger)
        {
            _GenreRepo = genreRepository;
            _logger = logger;

        }

        public async Task<int> CreateGenreAsync(GenreCreateDto dto)
        {
            var genre = dto.ToGenre();
            await _GenreRepo.Add(genre);
            return genre.Id;
        }

        public async Task<IEnumerable<GenreViewDto>> GetAllGenresAsync()
        {
            try
            {
                var genres = await _GenreRepo.GetAllIncludingAsync();
                return genres.Select(g => g.ToGenreViewDto());
            }
            catch (Exception ex) 
            {
                _logger.Log(ex.ToString());
                throw new GetAllGenresFailedException();
            }
        }

        public async Task<GenreViewDto> GetGenreByIdAsync(int id)
        {
            var genre = await _GenreRepo.GetByIdIncludingAsync(id);
            if (genre == null)
                throw new GenreNotFoundException(id);
            return genre.ToGenreViewDto();
        }

        public async Task UpdateGenreAsync(GenreUpdateDto dto)
        {
            try
            {
                var genre = await _GenreRepo.GetById(dto.Id);
                if (genre == null) return;

                genre.Name = dto.Name;
                await _GenreRepo.Update(genre);
            }
            catch (GenreNotFoundException) { throw; }
            catch (Exception ex) 
            {
                _logger.Log(ex.ToString());
                throw new GenreUpdateFailedException(dto.Id);
            }
        }

        public async Task<GenreDeleteDto> DeleteGenreAsync(int id)
        {
            var genre = await _GenreRepo.GetById(id);
            if (genre == null)
                throw new GenreNotFoundException(id);

            await _GenreRepo.Delete(id);

            return new GenreDeleteDto { Id = genre.Id };
        }
    }
}
