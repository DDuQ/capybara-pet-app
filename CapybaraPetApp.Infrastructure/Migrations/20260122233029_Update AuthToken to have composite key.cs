using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapybaraPetApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuthTokentohavecompositekey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "AuthToken",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthToken",
                table: "AuthToken",
                columns: new[] { "UserId", "RefreshToken" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthToken",
                table: "AuthToken");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "AuthToken",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
