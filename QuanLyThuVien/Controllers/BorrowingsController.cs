using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Dtos;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;


namespace QuanLyThuVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingsController : ControllerBase
    {
        private readonly IBorrowingService _borrowingService;
        private readonly IMapper _mapper;

        public BorrowingsController(IBorrowingService borrowingService
        , IMapper mapper
        )
        {
            _mapper = mapper;
            _borrowingService = borrowingService;
        }
        // GET: api/Borrowings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowingDto>>> GetAllBorrowings()
        {
            var borrowings = await _borrowingService.GetAllAsync();
            if (borrowings == null || !borrowings.Any())
            {
                return NotFound();
            } 
            var borrowingDtos = borrowings.Select(b => _mapper.Map<BorrowingDto>(b)).ToList();

            return Ok(borrowingDtos);
        }


        // GET: api/Borrowings/User/5
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<BorrowingDto>>> GetBorrowingsByUserId(int userId)
        {
            var borrowings = await _borrowingService.GetByUserIdAsync(userId);
            if (borrowings == null || !borrowings.Any())
            {
                return NotFound();
            }

            var borrowingDtos = borrowings.Select(b => _mapper.Map<BorrowingDto>(b)).ToList();


            return Ok(borrowingDtos);
        }

        // GET: api/Borrowings/5
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult<BorrowingDto>> GetBorrowingById(int id)
        {
            var borrowing = await _borrowingService.GetByIdAsync(id);

            if (borrowing == null)
            {
                return NotFound();
            }

            var borrowingDto = _mapper.Map<BorrowingDto>(borrowing);
         

            return Ok(borrowingDto);
        }

        // POST: api/Borrowings
        [HttpPost]
        [Authorize(Roles = "admin,user")]


        public async Task<ActionResult<BorrowingDto>> CreateBorrowing(BorrowingDto borrowingDto)
        {
            /*var borrowing = new Borrowing
            {
                UserID = borrowingDto.UserID,
                Status = borrowingDto.Status,
                BorrowDate = borrowingDto.BorrowDate,
                ReturnDate = (DateTime)borrowingDto.ReturnDate
            };*/
            var borrowing = _mapper.Map<Borrowing>(borrowingDto);
            await _borrowingService.AddAsync(borrowing);

            borrowingDto.BorrowingID = borrowing.BorrowingID;

            return CreatedAtAction(nameof(GetBorrowingById), new { id = borrowingDto.BorrowingID }, borrowingDto);
        }

        // PUT: api/Borrowings/5
        [HttpPut("{id}")]
        [Authorize("admin")]

        public async Task<IActionResult> UpdateBorrowing(int id, BorrowingDto borrowingDto)
        {
            if (id != borrowingDto.BorrowingID)
            {
                return BadRequest();
            }

            /*
            var borrowing = new Borrowing
            {
                BorrowingID = borrowingDto.BorrowingID,
                UserID = borrowingDto.UserID,
                Status = borrowingDto.Status,
                BorrowDate = borrowingDto.BorrowDate,
                ReturnDate = (DateTime)borrowingDto.ReturnDate
            };
            */
            var borrowing = _mapper.Map<Borrowing>(borrowingDto);

            await _borrowingService.UpdateAsync(borrowing);

            return NoContent();
        }

        // DELETE: api/Borrowings/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBorrowing(int id)
        {
            await _borrowingService.DeleteAsync(id);
            return NoContent();
        }

        // PUT: api/Borrowings/{id}/Return
        [HttpPut("{id}/Return")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> MarkBorrowingAsReturned(int id)
        {
            var borrowing = await _borrowingService.GetByIdAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            borrowing.Status = "Đã trả";
            borrowing.ReturnDate = System.DateTime.Now;

            await _borrowingService.UpdateAsync(borrowing);
            return NoContent();
        }
        [HttpPut("{id}/ReturnedAwaitingApproval")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> MarkReturnedAwaitingApproval(int id)
        {
            var borrowing = await _borrowingService.GetByIdAsync(id);

            if (borrowing == null)
            {
                return NotFound();
            }

            // Update the status to "Waiting for Return Approval"
            borrowing.Status = "Đang chờ phê duyệt trả"; // Or an appropriate status in your language
            borrowing.ReturnDate = DateTime.Now; // Record the date when the book was returned

            await _borrowingService.UpdateAsync(borrowing);
            return NoContent();

            
        }

        // PUT: api/Borrowings/{id}/ConfirmBorrowing
        [HttpPut("{id}/ConfirmBorrowing")]
        public async Task<IActionResult> ConfirmBorrowing(int id)
        {
            var borrowing = await _borrowingService.GetByIdAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            borrowing.Status = "Đã mượn";

            await _borrowingService.UpdateAsync(borrowing);
            return NoContent();
        }
    }
}
