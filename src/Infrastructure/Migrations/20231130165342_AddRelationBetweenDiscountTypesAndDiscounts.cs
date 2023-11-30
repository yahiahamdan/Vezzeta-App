using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenDiscountTypesAndDiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountTypeId",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_DiscountTypeId",
                table: "Discounts",
                column: "DiscountTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_DiscountTypes_DiscountTypeId",
                table: "Discounts",
                column: "DiscountTypeId",
                principalTable: "DiscountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_DiscountTypes_DiscountTypeId",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_DiscountTypeId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "DiscountTypeId",
                table: "Discounts");
        }
    }
}
