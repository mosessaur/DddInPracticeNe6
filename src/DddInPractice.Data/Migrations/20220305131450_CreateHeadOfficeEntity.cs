using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DddInPractice.Data.Migrations
{
    public partial class CreateHeadOfficeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeadOffice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OneCentCount = table.Column<int>(type: "int", nullable: false),
                    TenCentCount = table.Column<int>(type: "int", nullable: false),
                    QuarterCount = table.Column<int>(type: "int", nullable: false),
                    OneDollarCount = table.Column<int>(type: "int", nullable: false),
                    FiveDollarCount = table.Column<int>(type: "int", nullable: false),
                    TwentyDollarCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeadOffice", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Atm",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "FiveDollarCount", "OneCentCount", "OneDollarCount", "QuarterCount", "TenCentCount", "TwentyDollarCount" },
                values: new object[] { 0, 0, 0, 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "HeadOffice",
                columns: new[] { "Id", "Balance", "FiveDollarCount", "OneCentCount", "OneDollarCount", "QuarterCount", "TenCentCount", "TwentyDollarCount" },
                values: new object[] { 1L, 0m, 0, 0, 0, 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "SnackMachine",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "FiveDollarCount", "OneCentCount", "OneDollarCount", "QuarterCount", "TenCentCount", "TwentyDollarCount" },
                values: new object[] { 0, 0, 0, 0, 0, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeadOffice");

            migrationBuilder.UpdateData(
                table: "Atm",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "FiveDollarCount", "OneCentCount", "OneDollarCount", "QuarterCount", "TenCentCount", "TwentyDollarCount" },
                values: new object[] { 100, 100, 100, 100, 100, 100 });

            migrationBuilder.UpdateData(
                table: "SnackMachine",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "FiveDollarCount", "OneCentCount", "OneDollarCount", "QuarterCount", "TenCentCount", "TwentyDollarCount" },
                values: new object[] { 10, 10, 10, 10, 10, 10 });
        }
    }
}
