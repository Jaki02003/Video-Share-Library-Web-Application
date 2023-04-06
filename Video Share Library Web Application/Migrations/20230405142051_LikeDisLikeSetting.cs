using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Video_Share_Library_Web_Application.Migrations
{
    /// <inheritdoc />
    public partial class LikeDisLikeSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikeDislike",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Like = table.Column<bool>(type: "bit", nullable: false),
                    Dislike = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeDislike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeDislike_VideoInfo_VideoId",
                        column: x => x.VideoId,
                        principalTable: "VideoInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislike_VideoId",
                table: "LikeDislike",
                column: "VideoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeDislike");
        }
    }
}
