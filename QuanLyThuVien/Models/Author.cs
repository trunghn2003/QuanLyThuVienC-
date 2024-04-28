using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
    }
}
