using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using QuanLyThuVien.Services.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class BorrowedBooksController : ControllerBase
{
    private readonly IBorrowedBookService _borrowedBookService;

    public BorrowedBooksController(IBorrowedBookService borrowedBookService)
    {
        _borrowedBookService = borrowedBookService;
    }

    // GET: api/BorrowedBooks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BorrowedBookDto>>> GetBorrowedBook()
    {
        var borrowedBooks = await _borrowedBookService.GetAllAsync();
        var borrowedBookDtos = borrowedBooks.Select(bb => new BorrowedBookDto
        {
            BorrowedBookID = bb.BorrowedBookID,
            BorrowingID = bb.BorrowingID,
            BookID = bb.BookID
        }).ToList();

        return Ok(borrowedBookDtos);
    }

    // GET: api/BorrowedBooks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BorrowedBookDto>> GetBorrowedBook(int id)
    {
        var borrowedBook = await _borrowedBookService.GetByIdAsync(id);
        if (borrowedBook == null)
        {
            return NotFound();
        }

        var borrowedBookDto = new BorrowedBookDto
        {
            BorrowedBookID = borrowedBook.BorrowedBookID,
            BorrowingID = borrowedBook.BorrowingID,
            BookID = borrowedBook.BookID
        };

        return Ok(borrowedBookDto);
    }

    // POST: api/BorrowedBooks
    [HttpPost]
    public async Task<ActionResult<BorrowedBookDto>> PostBorrowedBook(BorrowedBookDto borrowedBookDto)
    {
        var borrowedBook = new BorrowedBook
        {
            BorrowingID = borrowedBookDto.BorrowingID,
            BookID = borrowedBookDto.BookID
        };

        await _borrowedBookService.AddAsync(borrowedBook);
        borrowedBookDto.BorrowedBookID = borrowedBook.BorrowedBookID;

        return CreatedAtAction(nameof(GetBorrowedBook), new { id = borrowedBook.BorrowedBookID }, borrowedBookDto);
    }

    // PUT: api/BorrowedBooks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBorrowedBook(int id, BorrowedBookDto borrowedBookDto)
    {
        if (id != borrowedBookDto.BorrowedBookID)
        {
            return BadRequest();
        }

        var borrowedBook = new BorrowedBook
        {
            BorrowedBookID = borrowedBookDto.BorrowedBookID,
            BorrowingID = borrowedBookDto.BorrowingID,
            BookID = borrowedBookDto.BookID
        };

        try
        {
            await _borrowedBookService.UpdateAsync(id, borrowedBook);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/BorrowedBooks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBorrowedBook(int id)
    {
        try
        {
            await _borrowedBookService.DeleteAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    // GET: api/BorrowedBooks/Borrowing/1
    [HttpGet("Borrowing/{borrowingId}")]
    public async Task<ActionResult<IEnumerable<BorrowedBookDto>>> GetBorrowedBooksByBorrowingId(int borrowingId)
    {
        var borrowedBooks = await _borrowedBookService.GetByBorrowingIdAsync(borrowingId);
        var borrowedBookDtos = borrowedBooks.Select(bb => new BorrowedBookDto
        {
            BorrowedBookID = bb.BorrowedBookID,
            BorrowingID = bb.BorrowingID,
            BookID = bb.BookID
        }).ToList();

        if (!borrowedBookDtos.Any())
        {
            return NotFound(new { message = $"No borrowed books found for borrowing ID {borrowingId}." });
        }

        return Ok(borrowedBookDtos);
    }

    
}
