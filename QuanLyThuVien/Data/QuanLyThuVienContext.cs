using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Data
{
    public class QuanLyThuVienContext : DbContext
    {
        public QuanLyThuVienContext (DbContextOptions<QuanLyThuVienContext> options)
            : base(options)
        {
        }

        public DbSet<QuanLyThuVien.Models.Author> Author { get; set; } = default!;
        public DbSet<QuanLyThuVien.Models.Genre> Genre { get; set; } = default!;
        public DbSet<QuanLyThuVien.Models.Book> Book { get; set; } = default!;
        public DbSet<QuanLyThuVien.Models.User> User { get; set; } = default!;
        public DbSet<QuanLyThuVien.Models.Borrowing> Borrowing { get; set; } = default!;
        public DbSet<QuanLyThuVien.Models.BorrowedBook> BorrowedBook { get; set; } = default!;
    }
}
