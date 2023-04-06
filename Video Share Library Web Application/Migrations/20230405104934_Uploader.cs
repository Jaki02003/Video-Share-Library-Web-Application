using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Video_Share_Library_Web_Application.Migrations
{
    /// <inheritdoc />
    public partial class Uploader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UploaderName",
                table: "VideoInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploaderName",
                table: "VideoInfo");
        }
    }
}
