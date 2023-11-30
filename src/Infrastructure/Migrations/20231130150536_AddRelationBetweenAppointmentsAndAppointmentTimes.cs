using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenAppointmentsAndAppointmentTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "AppointmentTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentTimes_AppointmentId",
                table: "AppointmentTimes",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentTimes_Appointments_AppointmentId",
                table: "AppointmentTimes",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentTimes_Appointments_AppointmentId",
                table: "AppointmentTimes");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentTimes_AppointmentId",
                table: "AppointmentTimes");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "AppointmentTimes");
        }
    }
}
