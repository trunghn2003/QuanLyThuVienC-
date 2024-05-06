using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository _borrowingRepository;

        public BorrowingService(IBorrowingRepository borrowingRepository)
        {
            _borrowingRepository = borrowingRepository;
        }
        public async Task<IEnumerable<Borrowing>> GetAllAsync()
        {
            return await _borrowingRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Borrowing>> GetByUserIdAsync(int userId)
        {
            return await _borrowingRepository.GetByUserIdAsync(userId);
        }

        public async Task<Borrowing> GetByIdAsync(int id)
        {
            return await _borrowingRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Borrowing borrowing)
        {
            await _borrowingRepository.AddAsync(borrowing);
        }

        public async Task UpdateAsync(Borrowing borrowing)
        {
            await _borrowingRepository.UpdateAsync(borrowing);
        }

        public async Task DeleteAsync(int id)
        {
            var borrowing = await _borrowingRepository.GetByIdAsync(id);
            if (borrowing != null)
            {
                await _borrowingRepository.DeleteAsync(borrowing);
            }
        }
    }
}
