using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KampusTek.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRegisterFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
