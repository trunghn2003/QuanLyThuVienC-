using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Dtos;
using QuanLyThuVien.Models;
namespace QuanLyThuVien.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBook(int id);
        Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthor(int authorId);
        Task<ActionResult<Book>> PostBook(BookCreateDTO bookDto);
        Task<bool> PutBook(int id, BookUpdateDTO bookDto);
        Task<bool> AddToCart(int id);
        Task<bool> RemoveFromCart(int id);
        Task<bool> DeleteBook(int id);
        bool BookExists(int id);
    }
}
