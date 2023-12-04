using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1b53555c-e5e3-4e66-ab07-26eb7f5f57e3", null, "Admin", null },
                    { "c4082530-a0e2-442d-8ddf-3473948e2add", null, "Patient", null },
                    { "f8cacecb-c5d7-4a10-a360-a42c4c8cc7aa", null, "Doctor", null }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0d893228-14b9-446d-b727-442a0d353458",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "49898182-20cd-4d75-a2d6-e6d95d1d565e", "8b0d81f1-6574-4e03-998a-bd48b76765bf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b53555c-e5e3-4e66-ab07-26eb7f5f57e3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4082530-a0e2-442d-8ddf-3473948e2add");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8cacecb-c5d7-4a10-a360-a42c4c8cc7aa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0d893228-14b9-446d-b727-442a0d353458",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d428b60a-30cd-4b17-9cee-110bbfba87be", "8f74d826-020a-4334-8021-794dfe248fd2" });
        }
    }
}
