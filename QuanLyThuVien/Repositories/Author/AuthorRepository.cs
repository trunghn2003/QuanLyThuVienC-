using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public class AuthorRepository : GenericRepository<Author>
    {
        private readonly QuanLyThuVienContext _context;
        public AuthorRepository(QuanLyThuVienContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
