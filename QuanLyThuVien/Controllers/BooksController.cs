using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly QuanLyThuVienContext _context;

        public BooksController(QuanLyThuVienContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Book.Include(b => b.Author).Include(b => b.Genre).ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Book.Include(b => b.Author).Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.BookID == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // POST: api/Books
        [HttpPost]
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

            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.BookID }, book);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDTO bookDto)
        {
            if (!BookExists(id))
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            book.Title = bookDto.Title ?? book.Title;
            book.AuthorID = bookDto.AuthorId ?? book.AuthorID;
            book.GenreID = bookDto.GenreId ?? book.GenreID;
            book.Status = bookDto.Status ?? book.Status;
            book.TotalQuantity = bookDto.TotalQuantity;
            book.RemainingQuantity = bookDto.RemainingQuantity;
            book.Image = bookDto.Image ?? book.Image;

            _context.Entry(book).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }
        [HttpPut("AddToCart/{id}")]
        public async Task<IActionResult> AddToCart(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Không tìm thấy sách." });
            }

            // Kiểm tra số lượng sách còn lại
            if (book.RemainingQuantity <= 0)
            {
                return BadRequest(new { message = "Sách đã hết hàng." });
            }

            // Giảm số lượng còn lại
            book.RemainingQuantity -= 1;

            _context.Entry(book).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "     ", RemainingQuantity = book.RemainingQuantity });
        }
        [HttpPut("RemoveFromCart/{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Không tìm thấy sách." });
            }

            

            // + 1 số lượng còn lại
            book.RemainingQuantity += 1;

            _context.Entry(book).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Sách đã được xóa khỏi giỏ hàng.", RemainingQuantity = book.RemainingQuantity });
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.BookID == id);
        }
        public class BookCreateDTO
        {
            [Required]
            [StringLength(100)]
            public string Title { get; set; }

            [Required]
            public int AuthorId { get; set; }

            [Required]
            public int GenreId { get; set; }

            [Required]
            public string Status { get; set; }

            [Range(1, int.MaxValue)]
            public int TotalQuantity { get; set; }

            [Range(0, int.MaxValue)]
            public int RemainingQuantity { get; set; }

            public string Image { get; set; }
        }

        public class BookUpdateDTO
        {
            [StringLength(100)]
            
            public string Title { get; set; }

            public int? AuthorId { get; set; }

            public int? GenreId { get; set; }

            public string Status { get; set; }

            [Range(1, int.MaxValue)]
            public int TotalQuantity { get; set; }

            [Range(0, int.MaxValue)]
            public int RemainingQuantity { get; set; }

            public string Image { get; set; }
        }
    }
}
