using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthor();
        Task<Author> GetAuthor(int id);
        Task<ActionResult<Author>> PostAuthor(Author author);
        Task<bool> PutAuthor(int id, Author author);
        Task<bool> DeleteAuthor(int id);
        bool AuthorExists(int id);
    }
}
