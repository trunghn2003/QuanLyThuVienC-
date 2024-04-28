using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        // Other relevant attributes
    }
}
