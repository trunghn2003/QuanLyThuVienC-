using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly QuanLyThuVienContext _context;
        public AuthorRepository(QuanLyThuVienContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Author author)
        {
            await _context.Author.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Author author)
        {
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return _context.Author.Any(c => c.AuthorID == id);
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Author.ToListAsync();

        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Author.FindAsync(id);
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
