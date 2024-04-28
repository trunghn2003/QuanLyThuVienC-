using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThuVien.Migrations
{
    /// <inheritdoc />
    public partial class init_borrowedbook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BorrowedBook",
                columns: table => new
                {
                    BorrowedBookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowingID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowedBook", x => x.BorrowedBookID);
                    table.ForeignKey(
                        name: "FK_BorrowedBook_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowedBook_Borrowing_BorrowingID",
                        column: x => x.BorrowingID,
                        principalTable: "Borrowing",
                        principalColumn: "BorrowingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBook_BookID",
                table: "BorrowedBook",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBook_BorrowingID",
                table: "BorrowedBook",
                column: "BorrowingID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowedBook");
        }
    }
}
