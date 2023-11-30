using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenTimesAndAppointmentTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeId",
                table: "AppointmentTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentTimes_TimeId",
                table: "AppointmentTimes",
                column: "TimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentTimes_Times_TimeId",
                table: "AppointmentTimes",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentTimes_Times_TimeId",
                table: "AppointmentTimes");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentTimes_TimeId",
                table: "AppointmentTimes");

            migrationBuilder.DropColumn(
                name: "TimeId",
                table: "AppointmentTimes");
        }
    }
}
