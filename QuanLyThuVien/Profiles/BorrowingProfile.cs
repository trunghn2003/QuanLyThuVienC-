using AutoMapper;
using QuanLyThuVien.Dtos;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Profiles;

public class BorrowingProfile : Profile
{
    public BorrowingProfile()
    {
        CreateMap<Borrowing, BorrowingDto>();
        CreateMap<BorrowingDto, Borrowing>();
    }
    
}

