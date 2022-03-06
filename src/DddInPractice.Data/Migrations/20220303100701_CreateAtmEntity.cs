using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DddInPractice.Data.Migrations
{
    public partial class CreateAtmEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atm",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OneCentCount = table.Column<int>(type: "int", nullable: false),
                    TenCentCount = table.Column<int>(type: "int", nullable: false),
                    QuarterCount = table.Column<int>(type: "int", nullable: false),
                    OneDollarCount = table.Column<int>(type: "int", nullable: false),
                    FiveDollarCount = table.Column<int>(type: "int", nullable: false),
                    TwentyDollarCount = table.Column<int>(type: "int", nullable: false),
                    MoneyCharged = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atm", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Atm",
                columns: new[] { "Id", "MoneyCharged", "FiveDollarCount", "OneCentCount", "OneDollarCount", "QuarterCount", "TenCentCount", "TwentyDollarCount" },
                values: new object[] { 1L, 0m, 100, 100, 100, 100, 100, 100 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atm");
        }
    }
}
