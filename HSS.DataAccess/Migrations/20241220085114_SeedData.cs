using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HSS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WebsiteUrl",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TaxIdentificationNumber",
                table: "Hospitals",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DiscoveryDate",
                table: "EffectiveSubstances",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "EffectiveSubstances",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ChemicalFormula",
                table: "EffectiveSubstances",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "AlternativeNames",
                table: "EffectiveSubstances",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.InsertData(
                table: "ClinicSpecializations",
                columns: new[] { "Id", "Description", "HospitalId", "IsDeleted", "Name" },
                values: new object[] { 1, "Heart-related treatments and procedures", null, false, "Cardiology" });

            migrationBuilder.InsertData(
                table: "IdentityUsers",
                columns: new[] { "Id", "ContactNumber", "CreatedAt", "Discriminator", "Email", "ExpirationOfRefreshToken", "HospitalAdmin_HospitalId", "IsDeleted", "Name", "NationalId", "Password", "RefreshToken", "Salt", "UpdatedAt" },
                values: new object[] { 1, null, new DateTime(2024, 12, 20, 8, 51, 14, 361, DateTimeKind.Utc).AddTicks(1770), "HospitalAdmin", "admin@hospital.com", null, 1, false, "Admin User", "12345678901234", "DuMNMjf83fqBhMioLbS2Mc3lOur1DtTQceSk63RWQX0=", null, "jcEClrARnDw8L47hrgbQyQ==", null });

            migrationBuilder.InsertData(
                table: "IdentityUsers",
                columns: new[] { "Id", "AgeCategory", "ContactNumber", "CreatedAt", "DateOfBirth", "Discriminator", "EducationLevel", "Email", "ExpirationOfRefreshToken", "IncomeCategory", "IsDeleted", "Name", "NationalId", "Password", "PatientMediacalDetailsId", "RefreshToken", "Salt", "Sex", "UpdatedAt" },
                values: new object[] { 4, null, "123-456-7890", new DateTime(2024, 12, 20, 8, 51, 14, 361, DateTimeKind.Utc).AddTicks(2108), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patient", null, "jane.doe@hospital.com", null, null, false, "Jane Doe", "33333333333333", "DuMNMjf83fqBhMioLbS2Mc3lOur1DtTQceSk63RWQX0=", null, null, "jcEClrARnDw8L47hrgbQyQ==", "Male", null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "IsDeleted", "RoleName" },
                values: new object[,]
                {
                    { 1, false, "Admin" },
                    { 2, false, "Receptionist" },
                    { 3, false, "Doctor" },
                    { 4, false, "Patient" }
                });

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "BedAvailability", "Capacity", "ContactNumber", "DepartmentsCount", "Email", "EndAt", "EstablishedDate", "HospitalAdminId", "IsDeleted", "Latitude", "LicenseNumber", "Location", "Longitude", "Name", "NumberOfDoctors", "NumberOfNurses", "Rating", "StartAt", "TaxIdentificationNumber", "WebsiteUrl" },
                values: new object[] { 1, 100, 300, "123-456-7890", 10, "info@centralhospital.com", new TimeSpan(0, 18, 0, 0, 0), new DateTime(1974, 12, 20, 10, 51, 14, 361, DateTimeKind.Local).AddTicks(1886), 1, false, 40.7128f, "HOSP123456", "123 Main St", -74.006f, "Central Hospital", 50, 100, 4.5f, new TimeSpan(0, 8, 0, 0, 0), "TAX123456", "http://www.centralhospital.com" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "Clinics",
                columns: new[] { "Id", "AppointmentDurationInMinutes", "FinishAt", "HospitalId", "IsDeleted", "Location", "SpecializationId", "SpecializationName", "StartAt" },
                values: new object[] { 1, 30, new TimeSpan(0, 17, 0, 0, 0), 1, false, "First Floor, Room 101", 1, "Cardiology", new TimeSpan(0, 9, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Receptions",
                columns: new[] { "Id", "EndAt", "HospitalId", "IsDeleted", "Location", "StartAt" },
                values: new object[] { 1, new TimeSpan(0, 18, 0, 0, 0), 1, false, "Ground Floor", new TimeSpan(0, 8, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "IdentityUsers",
                columns: new[] { "Id", "ContactNumber", "CreatedAt", "Discriminator", "Email", "ExpirationOfRefreshToken", "IsDeleted", "Name", "NationalId", "Password", "ReceptionId", "RefreshToken", "Salt", "UpdatedAt", "Receptionist_WorkingTime" },
                values: new object[] { 2, null, new DateTime(2024, 12, 20, 8, 51, 14, 361, DateTimeKind.Utc).AddTicks(1997), "Receptionist", null, null, false, "receptionist1", "11111111111111", "DuMNMjf83fqBhMioLbS2Mc3lOur1DtTQceSk63RWQX0=", 1, null, "jcEClrARnDw8L47hrgbQyQ==", null, new TimeSpan(0, 9, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "IdentityUsers",
                columns: new[] { "Id", "ClinicId", "ContactNumber", "CreatedAt", "Discriminator", "Email", "ExperienceYears", "ExpirationOfRefreshToken", "HireDate", "HospitalId", "IsDeleted", "Name", "NationalId", "Password", "RefreshToken", "Salt", "Specialization", "UpdatedAt", "WorkingTime" },
                values: new object[] { 3, 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doctor", "john.doe@hospital.com", 10, null, new DateTime(2024, 12, 20, 10, 51, 14, 361, DateTimeKind.Local).AddTicks(2074), 1, false, "Dr. John Doe", "22222222222222", "DuMNMjf83fqBhMioLbS2Mc3lOur1DtTQceSk63RWQX0=", null, "jcEClrARnDw8L47hrgbQyQ==", "Cardiology", null, new TimeSpan(0, 8, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 2, 2 },
                    { 3, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clinics",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Receptions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "WebsiteUrl",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TaxIdentificationNumber",
                table: "Hospitals",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DiscoveryDate",
                table: "EffectiveSubstances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "EffectiveSubstances",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChemicalFormula",
                table: "EffectiveSubstances",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AlternativeNames",
                table: "EffectiveSubstances",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
