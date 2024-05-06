using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyThuVien.Services
{
    public class BorrowedBookService : IBorrowedBookService
    {
        private readonly IBorrowedBookRepository _repository;

        public BorrowedBookService(IBorrowedBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BorrowedBook>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<BorrowedBook?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BorrowedBook>> GetByBorrowingIdAsync(int borrowingId)
        {
            return await _repository.GetByBorrowingIdAsync(borrowingId);
        }

        public async Task AddAsync(BorrowedBook borrowedBook)
        {
            await _repository.AddAsync(borrowedBook);
        }

        public async Task UpdateAsync(int id, BorrowedBook borrowedBook)
        {
            if (await _repository.ExistsAsync(id))
            {
                _repository.Update(borrowedBook);
            }
            else
            {
                throw new KeyNotFoundException("Borrowed book not found");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var borrowedBook = await _repository.GetByIdAsync(id);
            if (borrowedBook != null)
            {
                _repository.Delete(borrowedBook);
            }
            else
            {
                throw new KeyNotFoundException("Borrowed book not found");
            }
        }
    }
}
