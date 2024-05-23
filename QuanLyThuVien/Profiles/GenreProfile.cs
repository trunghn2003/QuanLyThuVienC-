using AutoMapper;
using QuanLyThuVien.Dtos;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreDto>();

            CreateMap<GenreDto, Genre>();
        }

    }
}
