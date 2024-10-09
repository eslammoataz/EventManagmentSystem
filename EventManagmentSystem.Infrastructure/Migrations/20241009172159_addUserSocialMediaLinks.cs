using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addUserSocialMediaLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSocialMediaLinks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Platform = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSocialMediaLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSocialMediaLinks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialMediaLinks_UserId",
                table: "UserSocialMediaLinks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSocialMediaLinks");
        }
    }
}
