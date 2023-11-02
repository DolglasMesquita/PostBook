using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostBook.Migrations
{
    public partial class listinlikespost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Likes_PostId",
                table: "Likes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Likes_PostId",
                table: "Likes",
                column: "PostId",
                unique: true);
        }
    }
}
