using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;


public class BorrowingDto
{
    public int BorrowingID { get; set; }
    public int UserID { get; set; }
    public string ? Status { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}

namespace QuanLyThuVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingsController : ControllerBase
    {
        private readonly QuanLyThuVienContext _context;

        public BorrowingsController(QuanLyThuVienContext context)
        {
            _context = context;
        }

        // GET: api/Borrowings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowingDto>>> GetBorrowing()
        {
            var borrowings = await _context.Borrowing.ToListAsync();
            var borrowingDtos = borrowings.Select(b => new BorrowingDto
            {
                BorrowingID = b.BorrowingID,
                Status = b.Status,
                UserID = b.UserID,
                BorrowDate = b.BorrowDate,
                ReturnDate = b.ReturnDate
            });

            return Ok(borrowingDtos);
        }

        // GET: api/Borrowings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowingDto>> GetBorrowing(int id)
        {
            var borrowing = await _context.Borrowing.FindAsync(id);

            if (borrowing == null)
            {
                return NotFound();
            }

            var borrowingDto = new BorrowingDto
            {
                BorrowingID = borrowing.BorrowingID,
                Status = borrowing.Status,
                UserID = borrowing.UserID,
                BorrowDate = borrowing.BorrowDate,
                ReturnDate = borrowing.ReturnDate
            };

            return borrowingDto;
        }

        // PUT: api/Borrowings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorrowing(int id, BorrowingDto borrowingDto)
        {
            if (id != borrowingDto.BorrowingID)
            {
                return BadRequest();
            }

            var borrowing = new Borrowing
            {
                BorrowingID = borrowingDto.BorrowingID,
                Status = borrowingDto.Status,
                UserID = borrowingDto.UserID,
                BorrowDate = borrowingDto.BorrowDate,
                ReturnDate = (DateTime)borrowingDto.ReturnDate
            };

            _context.Entry(borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(id))
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

        // POST: api/Borrowings
        [HttpPost]
        public async Task<ActionResult<BorrowingDto>> PostBorrowing(BorrowingDto borrowingDto)
        {
            var borrowing = new Borrowing
            {
                UserID = borrowingDto.UserID,
                Status = borrowingDto.Status,
                BorrowDate = borrowingDto.BorrowDate,
                ReturnDate = (DateTime)borrowingDto.ReturnDate
            };

            _context.Borrowing.Add(borrowing);
            await _context.SaveChangesAsync();

            var resultDto = new BorrowingDto
            {
                BorrowingID = borrowing.BorrowingID,
                UserID = borrowing.UserID,
                BorrowDate = borrowing.BorrowDate,
                ReturnDate = borrowing.ReturnDate
            };

            return CreatedAtAction("GetBorrowing", new { id = resultDto.BorrowingID }, resultDto);
        }

        // DELETE: api/Borrowings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowing(int id)
        {
            var borrowing = await _context.Borrowing.FindAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            _context.Borrowing.Remove(borrowing);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<BorrowingDto>>> GetBorrowingsByUserId(int userId)
        {
            var borrowings = await _context.Borrowing
                                          .Where(b => b.UserID == userId)
                                          .ToListAsync();

            if (borrowings == null || borrowings.Count == 0)
            {
                return NotFound();
            }

            var borrowingDtos = borrowings.Select(b => new BorrowingDto
            {
                BorrowingID = b.BorrowingID,
                UserID = b.UserID,
                Status = b.Status,
                BorrowDate = b.BorrowDate,
                ReturnDate = b.ReturnDate
            }).ToList();

            return Ok(borrowingDtos);
        }
        [HttpPut("{id}/Return")]
        public async Task<IActionResult> ReturnBorrowing(int id)
        {
            var borrowing = await _context.Borrowing.FindAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            // Cập nhật trạng thái và ngày trả sách
            borrowing.Status = "Đã trả";
            borrowing.ReturnDate = DateTime.Now;

            _context.Entry(borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); // Bạn cũng có thể trả về thông tin phiếu mượn đã cập nhật nếu cần
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        [HttpPut("{id}/ReturnedAwaitingApproval")]
        public async Task<IActionResult> MarkReturnedAwaitingApproval(int id)
        {
            var borrowing = await _context.Borrowing.FindAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            // Update the status to "Waiting for Return Approval"
            borrowing.Status = "Đang chờ phê duyệt trả"; // Or an appropriate status in your language
            borrowing.ReturnDate = DateTime.Now; // Record the date when the book was returned

            _context.Entry(borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); // You could also return the updated borrowing record if needed
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPut("{id}/ConfirmBorrowing")]
        public async Task<IActionResult> ConfirmBorrowing(int id)
        {
            var borrowing = await _context.Borrowing.FindAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            // Cập nhật trạng thái mượn sách
            borrowing.Status = "Đã mượn"; // hoặc một trạng thái thích hợp khác
            _context.Entry(borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); // hoặc bạn có thể trả về phiếu mượn đã cập nhật
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // PUT: api/Borrowings/{id}/ConfirmReturning
        [HttpPut("{id}/ConfirmReturning")]
        public async Task<IActionResult> ConfirmReturning(int id)
        {
            var borrowing = await _context.Borrowing.FindAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            // Cập nhật trạng thái trả sách
            borrowing.Status = "Returning Confirmed"; // hoặc một trạng thái thích hợp khác
            borrowing.ReturnDate = DateTime.Now;

            _context.Entry(borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); // hoặc bạn có thể trả về phiếu mượn đã cập nhật
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.BorrowingID == id);
        }

    }
    
}
