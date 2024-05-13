using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Models;
namespace QuanLyThuVien.Services
{
    public interface IBookService
    {
        Task<ActionResult<IEnumerable<Book>>> GetBooks();
        Task<ActionResult<Book>> GetBook(int id);
        Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthor(int authorId);
        Task<ActionResult<Book>> PostBook(BookCreateDTO bookDto);
        Task<bool> PutBook(int id, BookUpdateDTO bookDto);
        Task<bool> AddToCart(int id);
        Task<bool> RemoveFromCart(int id);
        Task<bool> DeleteBook(int id);
        bool BookExists(int id);
    }
}
