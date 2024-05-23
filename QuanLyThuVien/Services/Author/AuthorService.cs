using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly UnitOfWork _unitOfWork;
        public AuthorService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool AuthorExists(int id)
        {
            return _unitOfWork.AuthorRepository.Exists(id);
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return false;
            }
            await _unitOfWork.AuthorRepository.DeleteAsync(author);
             _unitOfWork.Save();
            return true;
        }

        public async Task<IEnumerable<Author>> GetAuthor()
        {
            var authors = await _unitOfWork.AuthorRepository.GetAllAsync();
            return authors;
        }

        public async Task<Author> GetAuthor(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
            return author;
        }

        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            await _unitOfWork.AuthorRepository.AddAsync(author);
            _unitOfWork.Save();


            return author;
        }

        public async Task<bool> PutAuthor(int id, Author author)
        {
            if (!AuthorExists(id))
            {
                return false;
            }
            var authorO = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
            authorO.AuthorName = author.AuthorName;
            await _unitOfWork.AuthorRepository.UpdateAsync(authorO);
            _unitOfWork.Save();

            return true;


        }
    }
}
