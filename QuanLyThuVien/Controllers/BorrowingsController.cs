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
    public class BorrowingsController : ControllerBase
    {
        private readonly QuanLyThuVienContext _context;

        public BorrowingsController(QuanLyThuVienContext context)
        {
            _context = context;
        }

        // GET: api/Borrowings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Borrowing>>> GetBorrowing()
        {
            return await _context.Borrowing.ToListAsync();
        }

        // GET: api/Borrowings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Borrowing>> GetBorrowing(int id)
        {
            var borrowing = await _context.Borrowing.FindAsync(id);

            if (borrowing == null)
            {
                return NotFound();
            }

            return borrowing;
        }

        // PUT: api/Borrowings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorrowing(int id, Borrowing borrowing)
        {
            if (id != borrowing.BorrowingID)
            {
                return BadRequest();
            }

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Borrowing>> PostBorrowing(Borrowing borrowing)
        {
            _context.Borrowing.Add(borrowing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBorrowing", new { id = borrowing.BorrowingID }, borrowing);
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

        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.BorrowingID == id);
        }
    }
}
