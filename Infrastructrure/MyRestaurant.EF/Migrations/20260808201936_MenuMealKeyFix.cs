using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRestaurant.EF.Migrations
{
    /// <inheritdoc />
    public partial class MenuMealKeyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuMeal",
                table: "MenuMeal");

            migrationBuilder.DropIndex(
                name: "IX_MenuMeal_MenuId",
                table: "MenuMeal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuMeal",
                table: "MenuMeal",
                columns: new[] { "MenuId", "MealPeriodId", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuMeal",
                table: "MenuMeal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuMeal",
                table: "MenuMeal",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MenuMeal_MenuId",
                table: "MenuMeal",
                column: "MenuId");
        }
    }
}
