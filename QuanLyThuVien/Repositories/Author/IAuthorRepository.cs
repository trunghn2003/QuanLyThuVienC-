using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author> GetByIdAsync(int id);
        Task<List<Author>> GetAllAsync();
        Task AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(Author author);
        bool Exists(int id);
    }
}
