using QuanLyThuVien.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyThuVien.Services
{
    public interface IBorrowedBookService
    {
        Task<IEnumerable<BorrowedBook>> GetAllAsync();
        Task<BorrowedBook?> GetByIdAsync(int id);
        Task<IEnumerable<BorrowedBook>> GetByBorrowingIdAsync(int borrowingId);
        Task AddAsync(BorrowedBook borrowedBook);
        Task UpdateAsync(int id, BorrowedBook borrowedBook);
        Task DeleteAsync(int id);
    }
}
