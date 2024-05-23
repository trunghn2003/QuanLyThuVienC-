namespace QuanLyThuVien.Dtos;

public class BorrowingDto
{
    public int BorrowingID { get; set; }
    public int UserID { get; set; }
    public string? Status { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}