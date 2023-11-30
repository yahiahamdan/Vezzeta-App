using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDayIdFromBookingsAndAppointmentsEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AppointmentDays_DayId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_DayId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "AppointmentDays",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AppointmentDays",
                newName: "Day");

            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DayId",
                table: "Bookings",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AppointmentDays_DayId",
                table: "Bookings",
                column: "DayId",
                principalTable: "AppointmentDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
