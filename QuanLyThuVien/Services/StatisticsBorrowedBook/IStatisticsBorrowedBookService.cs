using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services;

public interface IStatisticsBorrowedBookService
{
    Task<IEnumerable<StatisticsBorrowedBook>> GetAllAsync();
}