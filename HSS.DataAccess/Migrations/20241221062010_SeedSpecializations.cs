using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HSS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedSpecializations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SideEffects_Medicines_MedicineId",
                table: "SideEffects");

            migrationBuilder.DropIndex(
                name: "IX_SideEffects_MedicineId",
                table: "SideEffects");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "SideEffects");

            migrationBuilder.AddColumn<bool>(
                name: "IsStarted",
                table: "Appointments",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "علاجات وإجراءات متعلقة بالقلب", "أمراض القلب" });

            migrationBuilder.InsertData(
                table: "ClinicSpecializations",
                columns: new[] { "Id", "Description", "HospitalId", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 2, "علاجات وإجراءات متعلقة بالجلد", null, false, "الأمراض الجلدية" },
                    { 3, "علاجات الجهاز العصبي والدماغ", null, false, "الأعصاب" },
                    { 4, "علاجات العظام والمفاصل", null, false, "جراحة العظام" },
                    { 5, "الرعاية الصحية للأطفال", null, false, "طب الأطفال" },
                    { 6, "صحة الجهاز التناسلي للمرأة", null, false, "أمراض النساء" },
                    { 7, "رعاية العين والرؤية", null, false, "طب العيون" },
                    { 8, "علاجات السرطان", null, false, "الأورام" },
                    { 9, "علاجات الصحة النفسية", null, false, "الطب النفسي" },
                    { 10, "علاجات الجهاز البولي", null, false, "جراحة المسالك البولية" },
                    { 11, "علاجات الجهاز الهضمي", null, false, "أمراض الجهاز الهضمي" },
                    { 12, "علاجات متعلقة بالهرمونات", null, false, "أمراض الغدد الصماء" },
                    { 13, "علاجات الجهاز التنفسي والرئتين", null, false, "أمراض الرئة" },
                    { 14, "أمراض المفاصل والمناعة الذاتية", null, false, "أمراض الروماتيزم" },
                    { 15, "علاجات متعلقة بالكلى", null, false, "أمراض الكلى" },
                    { 16, "علاجات متعلقة بالدم", null, false, "أمراض الدم" },
                    { 17, "علاجات متعلقة بالعدوى", null, false, "الأمراض المعدية" },
                    { 18, "علاجات متعلقة بالحساسية والجهاز المناعي", null, false, "أمراض الحساسية والمناعة" },
                    { 19, "علاجات الأنف والأذن والحنجرة", null, false, "طب الأنف والأذن والحنجرة" },
                    { 20, "الجراحة التجميلية والترميمية", null, false, "جراحة التجميل" }
                });

            migrationBuilder.UpdateData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 1,
                column: "EstablishedDate",
                value: new DateTime(1974, 12, 21, 8, 20, 9, 796, DateTimeKind.Local).AddTicks(3592));

            migrationBuilder.UpdateData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "Salt" },
                values: new object[] { new DateTime(2024, 12, 21, 6, 20, 9, 796, DateTimeKind.Utc).AddTicks(3193), "QXG1iq/Y6azQChF2Zxu5mahfuWxsi21oXVUWEPfCBm8=", "yCO+h7P4PiwQp7rKKiy3mQ==" }); //@Aa123456789

            migrationBuilder.UpdateData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Password", "Salt" },
                values: new object[] { new DateTime(2024, 12, 21, 6, 20, 9, 796, DateTimeKind.Utc).AddTicks(3349), "QXG1iq/Y6azQChF2Zxu5mahfuWxsi21oXVUWEPfCBm8=", "yCO+h7P4PiwQp7rKKiy3mQ==" }); //@Aa123456789

            migrationBuilder.UpdateData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HireDate", "Password", "Salt" },
                values: new object[] { new DateTime(2024, 12, 21, 8, 20, 9, 796, DateTimeKind.Local).AddTicks(3379), "QXG1iq/Y6azQChF2Zxu5mahfuWxsi21oXVUWEPfCBm8=", "yCO+h7P4PiwQp7rKKiy3mQ==" }); //@Aa123456789

            migrationBuilder.UpdateData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Password", "Salt" },
                values: new object[] { new DateTime(2024, 12, 21, 6, 20, 9, 796, DateTimeKind.Utc).AddTicks(3449), "QXG1iq/Y6azQChF2Zxu5mahfuWxsi21oXVUWEPfCBm8=", "yCO+h7P4PiwQp7rKKiy3mQ==" }); //@Aa123456789
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DropColumn(
                name: "IsStarted",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "MedicineId",
                table: "SideEffects",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ClinicSpecializations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Heart-related treatments and procedures", "Cardiology" });

            migrationBuilder.UpdateData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 1,
                column: "EstablishedDate",
                value: new DateTime(1974, 12, 20, 10, 51, 14, 361, DateTimeKind.Local).AddTicks(1886));

            migrationBuilder.UpdateData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "Salt" },
                values: new object[] { new DateTime(2024, 12, 20, 8, 51, 14, 361, DateTimeKind.Utc).AddTicks(1770), "DuMNMjf83fqBhMioLbS2Mc3lOur1DtTQceSk63RWQX0=", "jcEClrARnDw8L47hrgbQyQ==" });

            migrationBuilder.UpdateData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Password", "Salt" },
                values: new object[] { new DateTime(2024, 12, 20, 8, 51, 14, 361, DateTimeKind.Utc).AddTicks(1997), "DuMNMjf83fqBhMioLbS2Mc3lOur1DtTQceSk63RWQX0=", "jcEClrARnDw8L47hrgbQyQ==" });

            migrationBuilder.UpdateData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HireDate", "Password", "Salt" },
                values: new object[] { new DateTime(2024, 12, 20, 10, 51, 14, 361, DateTimeKind.Local).AddTicks(2074), "DuMNMjf83fqBhMioLbS2Mc3lOur1DtTQceSk63RWQX0=", "jcEClrARnDw8L47hrgbQyQ==" });

            migrationBuilder.UpdateData(
                table: "IdentityUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Password", "Salt" },
                values: new object[] { new DateTime(2024, 12, 20, 8, 51, 14, 361, DateTimeKind.Utc).AddTicks(2108), "DuMNMjf83fqBhMioLbS2Mc3lOur1DtTQceSk63RWQX0=", "jcEClrARnDw8L47hrgbQyQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_SideEffects_MedicineId",
                table: "SideEffects",
                column: "MedicineId");

            migrationBuilder.AddForeignKey(
                name: "FK_SideEffects_Medicines_MedicineId",
                table: "SideEffects",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id");
        }
    }
}
