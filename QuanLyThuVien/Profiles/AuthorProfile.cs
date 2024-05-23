using AutoMapper;
using QuanLyThuVien.Dtos;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Profiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<AuthorDto, Author>();

        CreateMap<Author, AuthorDto>();
    }

}
