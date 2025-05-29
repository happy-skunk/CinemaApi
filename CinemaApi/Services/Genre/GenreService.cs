using CinemaApi.DTOs.Genre;
using CinemaApi.Mapper;
using CinemaApi.Repository.Specific;

namespace CinemaApi.Services.Genre
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _GenreRepo;

        public GenreService(IGenreRepository genreRepository)
        {
            _GenreRepo = genreRepository;
        }

        public async Task<int> CreateGenreAsync(GenreCreateDto dto)
        {
            var genre = dto.ToGenre();
            await _GenreRepo.Add(genre);
            return genre.Id;
        }

        public async Task<IEnumerable<GenreViewDto>> GetAllGenresAsync()
        {
            var genres = await _GenreRepo.GetAllIncludingAsync();
            return genres.Select(g => g.ToGenreViewDto());
        }

        public async Task<GenreViewDto> GetGenreByIdAsync(int id)
        {
            var genre = await _GenreRepo.GetByIdIncludingAsync(id);
            return genre?.ToGenreViewDto();
        }

        public async Task UpdateGenreAsync(GenreUpdateDto dto)
        {
            var genre = await _GenreRepo.GetById(dto.Id);
            if (genre == null) return;

            genre.Name = dto.Name;
            await _GenreRepo.Update(genre);
        }

        public async Task<GenreDeleteDto> DeleteGenreAsync(int id)
        {
            var genre = await _GenreRepo.GetById(id);
            if (genre == null) return null;

            await _GenreRepo.Delete(id);

            return new GenreDeleteDto
            {
                Id = genre.Id,
            };
        }
    }
}
