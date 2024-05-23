using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Dtos;
using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyThuVien.Services
{
    public class GenreService : IGenreService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

       

        public GenreService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public bool GenreExists(int id)
        {
            return _unitOfWork.GenreRepository.Exists(id);
        }

        public async Task<bool> DeleteGenre(int id)
        {
            var genre = await _unitOfWork.GenreRepository.GetByIdAsync(id);
            if (genre == null)
            {
                return false;
            }
            await _unitOfWork.GenreRepository.DeleteAsync(genre);
            _unitOfWork.Save();
            return true;
        }

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            var genres = await _unitOfWork.GenreRepository.GetAllAsync();
            
            return genres;
        }

        public async Task<Genre> GetGenre(int id)
        {
            var genre = await _unitOfWork.GenreRepository.GetByIdAsync(id);
            return genre;
        }

        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            await _unitOfWork.GenreRepository.AddAsync(genre);
            _unitOfWork.Save();
            return genre;
        }

        public async Task<bool> PutGenre(int id, Genre genre)
        {
            if (!GenreExists(id))
            {
                return false;
            }
            var existingGenre = await _unitOfWork.GenreRepository.GetByIdAsync(id);
            existingGenre.GenreName = genre.GenreName;
            await _unitOfWork.GenreRepository.UpdateAsync(existingGenre);
            _unitOfWork.Save();

            return true;
        }
    }
}
