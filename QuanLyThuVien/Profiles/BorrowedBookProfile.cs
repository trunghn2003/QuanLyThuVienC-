using AutoMapper;
using QuanLyThuVien.Dtos;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Profiles;

public class BorrowedBookProfile : Profile
{
    public BorrowedBookProfile()
    {
        CreateMap<BorrowedBook, BorrowedBookDto>();
        CreateMap<BorrowedBookDto, BorrowedBook>();
    }
    
}