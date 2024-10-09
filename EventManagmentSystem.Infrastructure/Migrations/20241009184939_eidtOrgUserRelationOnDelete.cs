using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class eidtOrgUserRelationOnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_AspNetUsers_AdminUserId",
                table: "Organizations");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_AspNetUsers_AdminUserId",
                table: "Organizations",
                column: "AdminUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_AspNetUsers_AdminUserId",
                table: "Organizations");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_AspNetUsers_AdminUserId",
                table: "Organizations",
                column: "AdminUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
