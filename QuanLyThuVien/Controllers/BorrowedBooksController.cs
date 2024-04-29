using System;
using System.Collections.Generic;
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
    public class BorrowedBooksController : ControllerBase
    {
        private readonly QuanLyThuVienContext _context;

        public BorrowedBooksController(QuanLyThuVienContext context)
        {
            _context = context;
        }

        // GET: api/BorrowedBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowedBookDTO>>> GetBorrowedBook()
        {
            var borrowedBooks = await _context.BorrowedBook
                .Include(b => b.Book)
                .Select(bb => new BorrowedBookDTO
                {
                    BorrowedBookID = bb.BorrowedBookID,
                    BorrowingID = bb.BorrowingID,
                    BookID = bb.BookID,
                })
                .ToListAsync();

            return Ok(borrowedBooks);
        }

        // GET: api/BorrowedBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowedBookDTO>> GetBorrowedBook(int id)
        {
            var borrowedBookDTO = await _context.BorrowedBook
                .Include(b => b.Book)
                .Where(bb => bb.BorrowedBookID == id)
                .Select(bb => new BorrowedBookDTO
                {
                    BorrowedBookID = bb.BorrowedBookID,
                    BorrowingID = bb.BorrowingID,
                    BookID = bb.BookID,
                })
                .FirstOrDefaultAsync();

            if (borrowedBookDTO == null)
            {
                return NotFound();
            }

            return borrowedBookDTO;
        }

        // PUT: api/BorrowedBooks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorrowedBook(int id, BorrowedBookDTO borrowedBookDTO)
        {
            if (id != borrowedBookDTO.BorrowedBookID)
            {
                return BadRequest();
            }

            var borrowedBook = await _context.BorrowedBook.FindAsync(id);
            if (borrowedBook == null)
            {
                return NotFound();
            }

            borrowedBook.BorrowingID = borrowedBookDTO.BorrowingID;
            borrowedBook.BookID = borrowedBookDTO.BookID;

            _context.Entry(borrowedBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowedBookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BorrowedBooks
        [HttpPost]
        public async Task<ActionResult<BorrowedBookDTO>> PostBorrowedBook(BorrowedBookDTO borrowedBookDTO)
        {
            var borrowedBook = new BorrowedBook
            {
                BorrowingID = borrowedBookDTO.BorrowingID,
                BookID = borrowedBookDTO.BookID
            };

            _context.BorrowedBook.Add(borrowedBook);
            await _context.SaveChangesAsync();

            borrowedBookDTO.BorrowedBookID = borrowedBook.BorrowedBookID;

            return CreatedAtAction("GetBorrowedBook", new { id = borrowedBook.BorrowedBookID }, borrowedBookDTO);
        }

        // DELETE: api/BorrowedBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowedBook(int id)
        {
            var borrowedBook = await _context.BorrowedBook.FindAsync(id);
            if (borrowedBook == null)
            {
                return NotFound();
            }

            _context.BorrowedBook.Remove(borrowedBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("Borrowing/{borrowingId}")]
        public async Task<ActionResult<IEnumerable<BorrowedBookDTO>>> GetBorrowedBooksByBorrowingId(int borrowingId)
        {
            var borrowedBooks = await _context.BorrowedBook
                .Include(bb => bb.Book)  // Optionally include Book details if needed
                .Where(bb => bb.BorrowingID == borrowingId)
                .Select(bb => new BorrowedBookDTO
                {
                    BorrowedBookID = bb.BorrowedBookID,
                    BorrowingID = bb.BorrowingID,
                    BookID = bb.BookID,
                    // Add more properties from the Book if needed
                })
                .ToListAsync();

            if (!borrowedBooks.Any())
            {
                return NotFound(new { message = $"No borrowed books found for borrowing ID {borrowingId}." });
            }

            return Ok(borrowedBooks);
        }

        private bool BorrowedBookExists(int id)
        {
            return _context.BorrowedBook.Any(e => e.BorrowedBookID == id);
        }
        public class BorrowedBookDTO
        {
            public int BorrowedBookID { get; set; }
            public int BorrowingID { get; set; }
            public int BookID { get; set; }
            // You can add details from the Book or Borrowing here if needed
        }
    }
}
