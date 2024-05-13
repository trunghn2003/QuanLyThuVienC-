using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Repositories;
using QuanLyThuVien.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
namespace QuanLyThuVien.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }


        public async Task<bool> AddToCart(int id)
        {
            if (!BookExists(id))
            {
                return false;
            }
            var book = await _bookRepository.GetByIdAsync(id);
            if (book.RemainingQuantity <= 0)
            {
                return false;
            }
            book.RemainingQuantity -= 1;
            await _bookRepository.UpdateAsync(book);
            return true;
            

        }

        public bool BookExists(int id)
        {
            return _bookRepository.Exists(id);
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if(book == null)
            {
                return false;
            }
           await _bookRepository.DeleteAsync(book);
            return true;

        }

        public async Task<ActionResult<Book>> GetBook(int id)
        {

            var book = await _bookRepository.GetByIdAsync(id);
            return book;


        }

        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return books; 
        }

        public async Task<ActionResult<Book>> PostBook(BookCreateDTO bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                AuthorID = bookDto.AuthorId,
                GenreID = bookDto.GenreId,
                Status = bookDto.Status,
                TotalQuantity = bookDto.TotalQuantity,
                RemainingQuantity = bookDto.RemainingQuantity,
                Image = bookDto.Image
            };
            await _bookRepository.AddAsync(book);

            return book;
        }

        public async Task<bool> PutBook(int id, BookUpdateDTO bookDto)
        {
            if (!BookExists(id))
            {
                return false;
            }

            var book = await _bookRepository.GetByIdAsync(id);
            book.Title = bookDto.Title ?? book.Title;
            book.AuthorID = bookDto.AuthorId ?? book.AuthorID;
            book.GenreID = bookDto.GenreId ?? book.GenreID;
            book.Status = bookDto.Status ?? book.Status;
            book.TotalQuantity = bookDto.TotalQuantity;
            book.RemainingQuantity = bookDto.RemainingQuantity;
            book.Image = bookDto.Image ?? book.Image;
            await _bookRepository.UpdateAsync(book);
            return true;
        }

        public async Task<bool> RemoveFromCart(int id)
        {
            if (!BookExists(id))
            {
                return false;
            }
            var book = await _bookRepository.GetByIdAsync(id);
            book.RemainingQuantity += 1;
           await _bookRepository.UpdateAsync(book);
            return true;
        }
        // write a method to get books by author
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthor(int authorId)
        {
            var books = await _bookRepository.GetBooksByAuthor(authorId);
            return books;
        }
    }
}
