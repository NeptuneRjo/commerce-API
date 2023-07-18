using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommerceClone.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAdminModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.RenameColumn(
                name: "AdminEmail",
                table: "Stores",
                newName: "OwnerEmail");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Carts",
                newName: "OwnerEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OwnerEmail",
                table: "Stores",
                newName: "AdminEmail");

            migrationBuilder.RenameColumn(
                name: "OwnerEmail",
                table: "Carts",
                newName: "UserEmail");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });
        }
    }
}
