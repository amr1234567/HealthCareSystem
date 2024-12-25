using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HSS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class LabTestConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "LabCenterTests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TestType",
                table: "LabCenterTests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LabCenterTests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "LabCenterTests");

            migrationBuilder.DropColumn(
                name: "TestType",
                table: "LabCenterTests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LabCenterTests");
        }
    }
}
