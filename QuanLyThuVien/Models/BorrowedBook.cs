using QuanLyThuVien.Models;   
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class BorrowedBook
    {
        [Key]
        public int BorrowedBookID { get; set; }
        public int BorrowingID { get; set; }
        public int BookID { get; set; }

        public virtual Borrowing ? Borrowing { get; set; }
        public virtual Book ?  Book { get; set; }
    }
}
