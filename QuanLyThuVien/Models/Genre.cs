using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }
        public string GenreName { get; set; }
    }
}
