using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyThuVien.Services
{
    public class BorrowedBookService : IBorrowedBookService
    {
        private readonly UnitOfWork _unitOfWork;

        public BorrowedBookService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }   
       

        public async Task<IEnumerable<BorrowedBook>> GetAllAsync()
        {
            return await _unitOfWork.BorrowedBookRepository.GetAllAsync();
        }

        public async Task<BorrowedBook?> GetByIdAsync(int id)
        {
            return await _unitOfWork.BorrowedBookRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BorrowedBook>> GetByBorrowingIdAsync(int borrowingId)
        {
            return await _unitOfWork.BorrowedBookRepository.GetByBorrowingIdAsync(borrowingId);
        }

        public async Task AddAsync(BorrowedBook borrowedBook)
        {
            await _unitOfWork.BorrowedBookRepository.AddAsync(borrowedBook);
            _unitOfWork.Save();
        }

        public async Task UpdateAsync(int id, BorrowedBook borrowedBook)
        {
            if ( _unitOfWork.BorrowedBookRepository.Exists(id))
            {
               await _unitOfWork.BorrowedBookRepository.UpdateAsync(borrowedBook);
                _unitOfWork.Save();
            }
            else
            {
                throw new KeyNotFoundException("Borrowed book not found");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var borrowedBook = await _unitOfWork.BorrowedBookRepository.GetByIdAsync(id);
            if (borrowedBook != null)
            {
                await _unitOfWork.BorrowedBookRepository.DeleteAsync(borrowedBook);
                _unitOfWork.Save();
                
            }
            else
            {
                throw new KeyNotFoundException("Borrowed book not found");
            }
        }
    }
}
