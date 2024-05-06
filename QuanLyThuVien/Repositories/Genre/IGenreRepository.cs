using QuanLyThuVien.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyThuVien.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre> GetByIdAsync(int id);
        Task<List<Genre>> GetAllAsync();
        Task AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task DeleteAsync(Genre genre);
        bool Exists(int id);
    }
}
