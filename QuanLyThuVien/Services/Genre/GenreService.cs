using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyThuVien.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public bool GenreExists(int id)
        {
            return _genreRepository.Exists(id);
        }

        public async Task<bool> DeleteGenre(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
            {
                return false;
            }
            await _genreRepository.DeleteAsync(genre);
            return true;
        }

        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            var genres = await _genreRepository.GetAllAsync();
            return genres;
        }

        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            return genre;
        }

        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            await _genreRepository.AddAsync(genre);
            return genre;
        }

        public async Task<bool> PutGenre(int id, Genre genre)
        {
            if (!GenreExists(id))
            {
                return false;
            }
            var existingGenre = await _genreRepository.GetByIdAsync(id);
            existingGenre.GenreName = genre.GenreName;
            await _genreRepository.UpdateAsync(existingGenre);
            return true;
        }
    }
}
