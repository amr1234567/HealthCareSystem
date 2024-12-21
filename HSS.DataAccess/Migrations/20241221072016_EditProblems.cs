using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HSS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditProblems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicSpecializations_Hospitals_HospitalId",
                table: "ClinicSpecializations");

            migrationBuilder.DropIndex(
                name: "IX_ClinicSpecializations_HospitalId",
                table: "ClinicSpecializations");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "ClinicSpecializations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "ClinicSpecializations",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 1,
                column: "HospitalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 2,
                column: "HospitalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 3,
                column: "HospitalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 4,
                column: "HospitalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 5,
                column: "HospitalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 6,
                column: "HospitalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 7,
                column: "HospitalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 8,
                column: "HospitalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 9,
                column: "HospitalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 10,
                column: "HospitalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 11,
                column: "HospitalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 13,
                column: "HospitalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 16,
                column: "HospitalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 19,
                column: "HospitalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 20,
                column: "HospitalId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_ClinicSpecializations_HospitalId",
                table: "ClinicSpecializations",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicSpecializations_Hospitals_HospitalId",
                table: "ClinicSpecializations",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id");
        }
    }
}
