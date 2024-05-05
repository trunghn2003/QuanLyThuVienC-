using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models;

public class Book
{
    [Key]
    public int BookID { get; set; }
    public string Title { get; set; }
    public int AuthorID { get; set; }
    public int GenreID { get; set; }
    public string Status { get; set; }
    public int TotalQuantity { get; set; }
    public int RemainingQuantity { get; set; }

    public virtual Author ? Author { get; set; }
    public virtual Genre ?  Genre { get; set; }
    public string Image {  get; set; }
}
