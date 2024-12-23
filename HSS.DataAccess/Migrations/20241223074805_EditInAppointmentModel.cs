using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HSS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditInAppointmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnd",
                table: "Appointments",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnd",
                table: "Appointments");
        }
    }
}
