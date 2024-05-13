using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;


        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }


        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return (await _bookService.GetBooks());
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetBook(id);

            if (!_bookService.BookExists(id))
            {
                return NotFound();
            }

            return book;
        }
        //Get api/Books/Author/5
        // write the GetBooksByAuthor method
        [HttpGet("Author/{id}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthor(int id)
        {
            return (await _bookService.GetBooksByAuthor(id));
        }

        // POST: api/Books
        [HttpPost]
        [Authorize("admin")]

        public async Task<ActionResult<Book>> PostBook(BookCreateDTO bookDto)
        {
            var newBook = await _bookService.PostBook(bookDto);


            return CreatedAtAction(nameof(GetBook), newBook);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        [Authorize("admin")]

        public async Task<IActionResult> PutBook(int id, BookUpdateDTO bookDto)
        {
            if (!await _bookService.PutBook(id, bookDto))
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPut("AddToCart/{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> AddToCart(int id)
        {
            

            if (!_bookService.BookExists(id))
            {
                return NotFound(new { message = "Không tìm thấy sách." });
            }
            if (!await _bookService.AddToCart(id))
            {
                return NotFound();
            }

            return NoContent();




        }
        [HttpPut("RemoveFromCart/{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            if (!_bookService.BookExists(id))
            {
                return NotFound(new { message = "Không tìm thấy sách." });
            }
            if (!await _bookService.RemoveFromCart(id))
            {
                return NotFound();
            }

            return NoContent();


        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize("admin")]

        public async Task<IActionResult> DeleteBook(int id)
        {
            if (!await _bookService.DeleteBook(id))
            {
                return NotFound();
            }
            return NoContent();
        }



    }
}
