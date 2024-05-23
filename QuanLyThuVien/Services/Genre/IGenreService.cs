using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyThuVien.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetGenres();
        Task<Genre> GetGenre(int id);
        Task<ActionResult<Genre>> PostGenre(Genre genre);
        Task<bool> PutGenre(int id, Genre genre);
        Task<bool> DeleteGenre(int id);
        bool GenreExists(int id);
    }
}
