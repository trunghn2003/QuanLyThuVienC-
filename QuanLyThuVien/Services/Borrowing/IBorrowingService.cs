using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    public interface IBorrowingService
    {
        Task<IEnumerable<Borrowing>> GetAllAsync();
        Task<IEnumerable<Borrowing>> GetByUserIdAsync(int userId);
        Task<Borrowing> GetByIdAsync(int id);
        Task AddAsync(Borrowing borrowing);
        Task UpdateAsync(Borrowing borrowing);
        Task DeleteAsync(int id);
    }
}
