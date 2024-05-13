using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly UnitOfWork _unitOfWork;

        public BorrowingService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }       
        
        public async Task<IEnumerable<Borrowing>> GetAllAsync()
        {
            return await _unitOfWork.BorrowingRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Borrowing>> GetByUserIdAsync(int userId)
        {
            return await _unitOfWork.BorrowingRepository.GetByUserIdAsync(userId);
        }

        public async Task<Borrowing> GetByIdAsync(int id)
        {
            return await _unitOfWork.BorrowingRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Borrowing borrowing)
        {
            await _unitOfWork.BorrowingRepository.AddAsync(borrowing);
            _unitOfWork.Save();
        }

        public async Task UpdateAsync(Borrowing borrowing)
        {
            await _unitOfWork.BorrowingRepository.UpdateAsync(borrowing);
            _unitOfWork.Save();

        }

        public async Task DeleteAsync(int id)
        {
            var borrowing = await _unitOfWork.BorrowingRepository.GetByIdAsync(id);
            if (borrowing != null)
            {
                await _unitOfWork.BorrowingRepository.DeleteAsync(borrowing);
                _unitOfWork.Save();

            }
        }
    }
}
