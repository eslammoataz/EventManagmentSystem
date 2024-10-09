using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editUserTable_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocialMedia",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SocialMedia",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
