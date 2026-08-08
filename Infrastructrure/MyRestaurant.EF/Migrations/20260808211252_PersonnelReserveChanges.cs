using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRestaurant.EF.Migrations
{
    /// <inheritdoc />
    public partial class PersonnelReserveChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonnelReservedOrderArticle");

            migrationBuilder.DropTable(
                name: "PersonnelReservedOrder");

            migrationBuilder.CreateTable(
                name: "PersonnelReserve",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    PersonnelId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelReserve", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelReserve_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonnelReservedMeal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MealPeriodId = table.Column<long>(type: "bigint", nullable: false),
                    PersonnelReserveId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<short>(type: "smallint", nullable: false),
                    IsReceived = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelReservedMeal", x => new { x.PersonnelReserveId, x.MealPeriodId, x.Id });
                    table.ForeignKey(
                        name: "FK_PersonnelReservedMeal_PersonnelReserve_PersonnelReserveId",
                        column: x => x.PersonnelReserveId,
                        principalTable: "PersonnelReserve",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelReserve_PersonnelId",
                table: "PersonnelReserve",
                column: "PersonnelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonnelReservedMeal");

            migrationBuilder.DropTable(
                name: "PersonnelReserve");

            migrationBuilder.CreateTable(
                name: "PersonnelReservedOrder",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PersonnelId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelReservedOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelReservedOrder_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonnelReservedOrderArticle",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PersonnelReservedOrderId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    IsReceived = table.Column<bool>(type: "bit", nullable: false),
                    MealId = table.Column<long>(type: "bigint", nullable: false),
                    MealPeriodId = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelReservedOrderArticle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelReservedOrderArticle_PersonnelReservedOrder_PersonnelReservedOrderId",
                        column: x => x.PersonnelReservedOrderId,
                        principalTable: "PersonnelReservedOrder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelReservedOrder_PersonnelId",
                table: "PersonnelReservedOrder",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelReservedOrderArticle_PersonnelReservedOrderId",
                table: "PersonnelReservedOrderArticle",
                column: "PersonnelReservedOrderId");
        }
    }
}
