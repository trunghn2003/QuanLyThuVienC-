using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyThuVien.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly QuanLyThuVienContext _context;

        public GenreRepository(QuanLyThuVienContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Genre genre)
        {
            await _context.Genre.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Genre genre)
        {
            _context.Genre.Remove(genre);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return _context.Genre.Any(g => g.GenreID == id);
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _context.Genre.ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genre.FindAsync(id);
        }

        public async Task UpdateAsync(Genre genre)
        {
            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
