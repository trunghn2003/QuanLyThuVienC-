using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Controllers;


[Route("api/[controller]")]
[ApiController]
public class StaticsticController : ControllerBase

{
    private readonly IStatisticsBorrowedBookService _statisticsBorrowedBookService;

    public StaticsticController(IStatisticsBorrowedBookService statisticsBorrowedBookService)
    {
        _statisticsBorrowedBookService = statisticsBorrowedBookService;
    }
    // GET: api/Statisctics
    [HttpGet]
   public async Task<ActionResult<IEnumerable<StatisticsBorrowedBook>>> GetStatistics()
        {
            return (await _statisticsBorrowedBookService.GetAllAsync()).ToList();
        }
    

}