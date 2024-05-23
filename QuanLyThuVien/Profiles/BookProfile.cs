using AutoMapper;
using QuanLyThuVien.Dtos;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<BookDto, Book>();
        CreateMap<BookCreateDTO, Book>();
        CreateMap<BookUpdateDTO, Book>();
    }
    
}