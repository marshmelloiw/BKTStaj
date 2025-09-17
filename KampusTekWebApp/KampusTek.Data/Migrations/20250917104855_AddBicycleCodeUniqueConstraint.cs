using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KampusTek.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBicycleCodeUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bicycles_Stations_CurrentStationId",
                table: "Bicycles");

            migrationBuilder.AlterColumn<string>(
                name: "BicycleCode",
                table: "Bicycles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Student");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Staff");

            migrationBuilder.CreateIndex(
                name: "IX_Bicycles_BicycleCode",
                table: "Bicycles",
                column: "BicycleCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bicycles_Stations_CurrentStationId",
                table: "Bicycles",
                column: "CurrentStationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bicycles_Stations_CurrentStationId",
                table: "Bicycles");

            migrationBuilder.DropIndex(
                name: "IX_Bicycles_BicycleCode",
                table: "Bicycles");

            migrationBuilder.AlterColumn<string>(
                name: "BicycleCode",
                table: "Bicycles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Öğrenci");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Personel");

            migrationBuilder.AddForeignKey(
                name: "FK_Bicycles_Stations_CurrentStationId",
                table: "Bicycles",
                column: "CurrentStationId",
                principalTable: "Stations",
                principalColumn: "Id");
        }
    }
}
