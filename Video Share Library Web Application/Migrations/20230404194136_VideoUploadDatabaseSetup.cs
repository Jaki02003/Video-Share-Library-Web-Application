using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Video_Share_Library_Web_Application.Migrations
{
    /// <inheritdoc />
    public partial class VideoUploadDatabaseSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "VideoInfo",
                newName: "VideoPath");

            migrationBuilder.AddColumn<string>(
                name: "VideoName",
                table: "VideoInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoName",
                table: "VideoInfo");

            migrationBuilder.RenameColumn(
                name: "VideoPath",
                table: "VideoInfo",
                newName: "title");
        }
    }
}
