﻿using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyThuVien.Repositories
{
    public class BorrowedBookRepository : GenericRepository<BorrowedBook>
    {
        private readonly QuanLyThuVienContext _context;

        public BorrowedBookRepository(QuanLyThuVienContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BorrowedBook>> GetByBorrowingIdAsync(int borrowingId)
        {
            return await _context.BorrowedBook
                .Include(b => b.Book)
                .Where(b => b.BorrowingID == borrowingId)
                .ToListAsync();
        }

       
        /*public void Update(BorrowedBook borrowedBook)
        {
            _context.Entry(borrowedBook).State = EntityState.Modified;
        }

        public void Delete(BorrowedBook borrowedBook)
        {
            _context.BorrowedBook.Remove(borrowedBook);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.BorrowedBook.AnyAsync(b => b.BorrowedBookID == id);
        }*/
    }
}
