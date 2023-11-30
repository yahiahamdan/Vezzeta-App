using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenBookingsAndBookingStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_StatusId",
                table: "Bookings",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_BookingStatus_StatusId",
                table: "Bookings",
                column: "StatusId",
                principalTable: "BookingStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_BookingStatus_StatusId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_StatusId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Bookings");
        }
    }
}
