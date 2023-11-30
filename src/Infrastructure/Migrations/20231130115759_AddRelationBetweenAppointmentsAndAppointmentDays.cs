using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenAppointmentsAndAppointmentDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DayId",
                table: "Appointments",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentDays_DayId",
                table: "Appointments",
                column: "DayId",
                principalTable: "AppointmentDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentDays_DayId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DayId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "Appointments");
        }
    }
}
