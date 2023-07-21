using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommerceClone.Migrations
{
    /// <inheritdoc />
    public partial class Keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConnectionString",
                table: "Stores",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "PublicKey",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecretKey",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicKey",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "SecretKey",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Stores",
                newName: "ConnectionString");
        }
    }
}
