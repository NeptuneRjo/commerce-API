using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommerceClone.Migrations
{
    /// <inheritdoc />
    public partial class LessReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Admins_AdminId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_AdminId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Stores");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Carts",
                newName: "UserEmail");

            migrationBuilder.AddColumn<string>(
                name: "AdminEmail",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminEmail",
                table: "Stores");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Carts",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AdminId",
                table: "Stores",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Admins_AdminId",
                table: "Stores",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
