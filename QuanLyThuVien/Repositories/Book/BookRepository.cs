using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public class BookRepository : GenericRepository<Book>
    {
        private readonly QuanLyThuVienContext  _context;
        public BookRepository(QuanLyThuVienContext context) : base(context)
        {
            _context = context;
        }
        // implement the GetBooksByAuthor method
        public async Task<List<Book>> GetBooksByAuthor(int id)
        {
            return await _context.Book.Where(b => b.AuthorID == id).ToListAsync();
        }
        
    }
}
