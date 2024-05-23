using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Dtos;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

[Route("api/[controller]")]
[ApiController]
public class BorrowedBooksController : ControllerBase
{
    private readonly IBorrowedBookService _borrowedBookService;
    private readonly IMapper _mapper;

    public BorrowedBooksController(IBorrowedBookService borrowedBookService,
        IMapper mapper
        )
    {
        _mapper = mapper;
        _borrowedBookService = borrowedBookService;
    }

    // GET: api/BorrowedBooks
    [HttpGet]

    public async Task<ActionResult<IEnumerable<BorrowedBookDto>>> GetBorrowedBook()
    {
        var borrowedBooks = await _borrowedBookService.GetAllAsync();
        var borrowedBookDtos = borrowedBooks.Select(bb => _mapper.Map<BorrowedBookDto>(bb)).ToList();
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

        var borrowedBookDto = _mapper.Map<BorrowedBookDto>(borrowedBook);
        return Ok(borrowedBookDto);
    }

    // POST: api/BorrowedBooks
    [HttpPost]
    [Authorize(Roles = "admin,user")]


    public async Task<ActionResult<BorrowedBookDto>> PostBorrowedBook(BorrowedBookDto borrowedBookDto)
    {
        var borrowedBook = _mapper.Map<BorrowedBook>(borrowedBookDto);

        await _borrowedBookService.AddAsync(borrowedBook);
        borrowedBookDto.BorrowedBookID = borrowedBook.BorrowedBookID;

        return CreatedAtAction(nameof(GetBorrowedBook), new { id = borrowedBook.BorrowedBookID }, borrowedBookDto);
    }

    // PUT: api/BorrowedBooks/5
    [HttpPut("{id}")]
    [Authorize(Roles = "admin,user")]

    public async Task<IActionResult> PutBorrowedBook(int id, BorrowedBookDto borrowedBookDto)
    {
        if (id != borrowedBookDto.BorrowedBookID)
        {
            return BadRequest();
        }
        var borrowedBook = _mapper.Map<BorrowedBook>(borrowedBookDto);

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
    [Authorize("admin")]

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
    //[Authorize(Roles = "admin,user")]

    public async Task<ActionResult<IEnumerable<BorrowedBookDto>>> GetBorrowedBooksByBorrowingId(int borrowingId)
    {
        var borrowedBooks = await _borrowedBookService.GetByBorrowingIdAsync(borrowingId);
        var borrowedBookDtos = borrowedBooks.Select(bb => _mapper.Map<BorrowedBookDto>(bb)).ToList();
        if (!borrowedBookDtos.Any())
        {
            return NotFound(new { message = $"No borrowed books found for borrowing ID {borrowingId}." });
        }

        return Ok(borrowedBookDtos);
    }


}
