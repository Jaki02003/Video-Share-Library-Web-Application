using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Video_Share_Library_Web_Application.Migrations
{
    /// <inheritdoc />
    public partial class LikeDisLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "VideoInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "VideoInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "VideoInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "VideoInfo");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "VideoInfo");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "VideoInfo");
        }
    }
}
