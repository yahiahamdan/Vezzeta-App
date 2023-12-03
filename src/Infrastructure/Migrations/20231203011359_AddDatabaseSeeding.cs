using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabaseSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "Image", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SpecializationId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0d893228-14b9-446d-b727-442a0d353458", 0, "d428b60a-30cd-4b17-9cee-110bbfba87be", "18/03/1999", "sragmahmoud4@gmail.com", false, "Mahmoud", "Male", null, "Serag", false, null, null, null, "Admin123", "+201064560413", false, "8f74d826-020a-4334-8021-794dfe248fd2", null, false, null });

            migrationBuilder.InsertData(
                table: "BookingStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Completed" },
                    { 3, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Saturday" },
                    { 2, "Sunday" },
                    { 3, "Monday" },
                    { 4, "Tuesday" },
                    { 5, "Wednesday" },
                    { 6, "Thursday" },
                    { 7, "Friday" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0d893228-14b9-446d-b727-442a0d353458");

            migrationBuilder.DeleteData(
                table: "BookingStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookingStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookingStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Days",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
