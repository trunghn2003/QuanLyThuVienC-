using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyThuVien.Repositories
{
    public class GenreRepository : GenericRepository<Genre>
    {
        private readonly QuanLyThuVienContext _context;

        public GenreRepository(QuanLyThuVienContext context) : base(context)
        {
            _context = context;
        }
        public bool Exists(int id)
        {
            return _context.Genre.Any(g => g.GenreID == id);
        }
    }
}
