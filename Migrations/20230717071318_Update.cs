using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommerceClone.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Oauth",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Admins",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Admins",
                newName: "Username");

            migrationBuilder.AddColumn<int>(
                name: "Oauth",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
