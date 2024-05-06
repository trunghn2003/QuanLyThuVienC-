using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly QuanLyThuVienContext _context;
        public async Task<IEnumerable<Borrowing>> GetAllAsync()
        {
            return await _context.Borrowing.ToListAsync();
        }
        public BorrowingRepository(QuanLyThuVienContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Borrowing>> GetByUserIdAsync(int userId)
        {
            return await _context.Borrowing
                .Where(b => b.UserID == userId)
                .ToListAsync();
        }

        public async Task<Borrowing> GetByIdAsync(int id)
        {
            return await _context.Borrowing.FindAsync(id);
        }

        public async Task AddAsync(Borrowing borrowing)
        {
            await _context.Borrowing.AddAsync(borrowing);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Borrowing borrowing)
        {
            _context.Entry(borrowing).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Borrowing borrowing)
        {
            _context.Borrowing.Remove(borrowing);
            await _context.SaveChangesAsync();
        }
    }
}
