namespace QuanLyThuVien.Models
{
    public class StatisticsBorrowedBook
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int BorrowedCount { get; set; }
        public string AuthorName { get; set; }
        public string GenreName { get; set; }
    }
}
