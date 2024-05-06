using QuanLyThuVien.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyThuVien.Repositories
{
    public interface IBorrowingRepository
    {
        Task<IEnumerable<Borrowing>> GetAllAsync();
        Task<IEnumerable<Borrowing>> GetByUserIdAsync(int userId);
        Task<Borrowing> GetByIdAsync(int id);
        Task AddAsync(Borrowing borrowing);
        Task UpdateAsync(Borrowing borrowing);
        Task DeleteAsync(Borrowing borrowing);
    }
}
