﻿using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Dtos;

public class BookDto

{
    public int BookID { get; set; }
    public string Title { get; set; }
    public int AuthorID { get; set; }
    public int GenreID { get; set; }
    public string Status { get; set; }
    public int TotalQuantity { get; set; }
    public int RemainingQuantity { get; set; }
    public string Image {  get; set; }

}
public class BookCreateDTO
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    public int AuthorId { get; set; }

    [Required]
    public int GenreId { get; set; }

    [Required]
    public string Status { get; set; }

    [Range(1, int.MaxValue)]
    public int TotalQuantity { get; set; }

    [Range(0, int.MaxValue)]
    public int RemainingQuantity { get; set; }

    public string Image { get; set; }
}

public class BookUpdateDTO
{
    
    [StringLength(100)]

    public string Title { get; set; }

    public int? AuthorId { get; set; }

    public int? GenreId { get; set; }

    public string Status { get; set; }

    [Range(1, int.MaxValue)]
    public int TotalQuantity { get; set; }

    [Range(0, int.MaxValue)]
    public int RemainingQuantity { get; set; }

    public string Image { get; set; }
}