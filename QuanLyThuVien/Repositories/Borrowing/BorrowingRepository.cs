using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public class BorrowingRepository : GenericRepository<Borrowing>
    {
        private readonly QuanLyThuVienContext _context;
       
        public BorrowingRepository(QuanLyThuVienContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Borrowing>> GetByUserIdAsync(int userId)
        {
            return await _context.Borrowing
                .Where(b => b.UserID == userId)
                .ToListAsync();
        }
      

      
    }
}
