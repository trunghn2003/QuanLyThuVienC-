using QuanLyThuVien.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyThuVien.Repositories
{
    public interface IBorrowedBookRepository
    {
        Task<IEnumerable<BorrowedBook>> GetAllAsync();
        Task<BorrowedBook?> GetByIdAsync(int id);
        Task<IEnumerable<BorrowedBook>> GetByBorrowingIdAsync(int borrowingId);
        Task AddAsync(BorrowedBook borrowedBook);
        void Update(BorrowedBook borrowedBook);
        void Delete(BorrowedBook borrowedBook);
        Task<bool> ExistsAsync(int id);
    }
}
