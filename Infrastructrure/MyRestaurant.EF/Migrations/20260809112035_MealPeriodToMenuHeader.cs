using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRestaurant.EF.Migrations
{
    /// <inheritdoc />
    public partial class MealPeriodToMenuHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonnelReservedMeal",
                table: "PersonnelReservedMeal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuMeal",
                table: "MenuMeal");

            migrationBuilder.DropColumn(
                name: "MealPeriodId",
                table: "PersonnelReservedMeal");

            migrationBuilder.DropColumn(
                name: "MealPeriodId",
                table: "MenuMeal");

            migrationBuilder.AddColumn<long>(
                name: "MealPeriodId",
                table: "PersonnelReserve",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MealPeriodId",
                table: "Menus",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonnelReservedMeal",
                table: "PersonnelReservedMeal",
                columns: new[] { "PersonnelReserveId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuMeal",
                table: "MenuMeal",
                columns: new[] { "MenuId", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonnelReservedMeal",
                table: "PersonnelReservedMeal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuMeal",
                table: "MenuMeal");

            migrationBuilder.DropColumn(
                name: "MealPeriodId",
                table: "PersonnelReserve");

            migrationBuilder.DropColumn(
                name: "MealPeriodId",
                table: "Menus");

            migrationBuilder.AddColumn<long>(
                name: "MealPeriodId",
                table: "PersonnelReservedMeal",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MealPeriodId",
                table: "MenuMeal",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonnelReservedMeal",
                table: "PersonnelReservedMeal",
                columns: new[] { "PersonnelReserveId", "MealPeriodId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuMeal",
                table: "MenuMeal",
                columns: new[] { "MenuId", "MealPeriodId", "Id" });
        }
    }
}
