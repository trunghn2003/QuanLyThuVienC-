    using System.ComponentModel.DataAnnotations;

    namespace QuanLyThuVien.Models
    {
        public class Borrowing

        {
            [Key]
            public int BorrowingID { get; set; }
            public int UserID { get; set; }
            public DateTime BorrowDate { get; set; }
            public DateTime ReturnDate { get; set; }
            public string Status { get; set; }
            public virtual  User ? User { get; set; }
        }
    }
