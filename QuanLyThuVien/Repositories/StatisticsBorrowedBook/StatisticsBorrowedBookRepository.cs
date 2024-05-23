using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyThuVien.Repositories
{
    public class StatisticsBorrowedBookRepository : GenericRepository<StatisticsBorrowedBook>
    {
        private readonly QuanLyThuVienContext _context;

        public StatisticsBorrowedBookRepository(QuanLyThuVienContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StatisticsBorrowedBook>> GetAllAsync()
        {
            // Retrieve all borrowed books with related Book, Author, and Genre data
            var borrowedBooks = await _context.BorrowedBook
                .Include(b => b.Book)
                .ThenInclude(book => book.Author)
                .Include(b => b.Book)
                .ThenInclude(book => book.Genre)
                .ToListAsync();

            // Group by BookID, BookName, AuthorName, and GenreName and project into StatisticsBorrowedBook
            var statistics = borrowedBooks
                .GroupBy(b => new { b.Book.BookID, b.Book.Title, 
                    AuthorName = b.Book.Author.AuthorName, GenreName = b.Book.Genre.GenreName })
                .Select(g => new StatisticsBorrowedBook
                {
                    BookId = g.Key.BookID,
                    BookName = g.Key.Title,
                    BorrowedCount = g.Count(),
                    AuthorName = g.Key.AuthorName,
                    GenreName = g.Key.GenreName
                }).ToList();

            return statistics;
        }
    }
}