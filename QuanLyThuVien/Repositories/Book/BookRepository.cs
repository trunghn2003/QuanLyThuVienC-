using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly QuanLyThuVienContext  _context;
        public BookRepository(QuanLyThuVienContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Book book)
        {
             await _context.Book.AddAsync(book);
             await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return _context.Book.Any(c => c.BookID == id);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Book.ToListAsync();

        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Book.FindAsync(id);
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
