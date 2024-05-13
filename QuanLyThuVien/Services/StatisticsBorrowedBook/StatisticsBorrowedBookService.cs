using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Services;

public class StatisticsBorrowedBookService: IStatisticsBorrowedBookService
{
    private readonly UnitOfWork _unitOfWork;

    public StatisticsBorrowedBookService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    
    }
    public async Task<IEnumerable<StatisticsBorrowedBook>> GetAllAsync()
    {
        var data = await _unitOfWork.StatisticsBorrowedBookRepository.GetAllAsync();
        return data;
    }
}