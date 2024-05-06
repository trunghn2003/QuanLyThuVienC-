using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public bool AuthorExists(int id)
        {
            return _authorRepository.Exists(id);
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return false;
            }
            await _authorRepository.DeleteAsync(author);
            return true;
        }

        public async Task<ActionResult<IEnumerable<Author>>> GetAuthor()
        {
            var authors = await _authorRepository.GetAllAsync();
            return authors;
        }

        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return author;
        }

        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            await _authorRepository.AddAsync(author);

            return author;
        }

        public async Task<bool> PutAuthor(int id, Author author)
        {
            if (!AuthorExists(id))
            {
                return false;
            }
            var authorO = await _authorRepository.GetByIdAsync(id);
            authorO.AuthorName = author.AuthorName;
            await _authorRepository.UpdateAsync(authorO);
            return true;


        }
    }
}
