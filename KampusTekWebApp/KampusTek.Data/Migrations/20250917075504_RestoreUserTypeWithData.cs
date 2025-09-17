using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KampusTek.Data.Migrations
{
    /// <inheritdoc />
    public partial class RestoreUserTypeWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // First create the UserTypes table
            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            // Insert seed data
            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Öğrenci" },
                    { 2, "Personel" }
                });

            // Add UserTypeId column to Users table with default value 1
            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 1);

            // Create index
            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                table: "Users",
                column: "UserTypeId");

            // Add foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTypes_UserTypeId",
                table: "Users",
                column: "UserTypeId",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTypes_UserTypeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserTypeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserTypes");
        }
    }
}
