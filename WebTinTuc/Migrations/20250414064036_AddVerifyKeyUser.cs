using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTinTuc.Migrations
{
    /// <inheritdoc />
    public partial class AddVerifyKeyUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VerifyKey",
                table: "Users",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerifyKey",
                table: "Users");
        }
    }
}
