using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HSS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecializationForHospital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "clinicSpecializationHospitals",
                columns: new[] { "ClinicSpecializationId", "HospitalId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 16, 1 },
                    { 19, 1 },
                    { 20, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "clinicSpecializationHospitals",
                keyColumns: new[] { "ClinicSpecializationId", "HospitalId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "clinicSpecializationHospitals",
                keyColumns: new[] { "ClinicSpecializationId", "HospitalId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "clinicSpecializationHospitals",
                keyColumns: new[] { "ClinicSpecializationId", "HospitalId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "clinicSpecializationHospitals",
                keyColumns: new[] { "ClinicSpecializationId", "HospitalId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "clinicSpecializationHospitals",
                keyColumns: new[] { "ClinicSpecializationId", "HospitalId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "clinicSpecializationHospitals",
                keyColumns: new[] { "ClinicSpecializationId", "HospitalId" },
                keyValues: new object[] { 16, 1 });

            migrationBuilder.DeleteData(
                table: "clinicSpecializationHospitals",
                keyColumns: new[] { "ClinicSpecializationId", "HospitalId" },
                keyValues: new object[] { 19, 1 });

            migrationBuilder.DeleteData(
                table: "clinicSpecializationHospitals",
                keyColumns: new[] { "ClinicSpecializationId", "HospitalId" },
                keyValues: new object[] { 20, 1 });
        }
    }
}
