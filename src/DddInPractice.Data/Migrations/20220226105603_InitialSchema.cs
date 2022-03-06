using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DddInPractice.Data.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SnackMachine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OneCentCount = table.Column<int>(type: "int", nullable: false),
                    TenCentCount = table.Column<int>(type: "int", nullable: false),
                    QuarterCount = table.Column<int>(type: "int", nullable: false),
                    OneDollarCount = table.Column<int>(type: "int", nullable: false),
                    FiveDollarCount = table.Column<int>(type: "int", nullable: false),
                    TwentyDollarCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnackMachine", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SnackMachine",
                columns: new[] { "Id", "FiveDollarCount", "OneCentCount", "OneDollarCount", "QuarterCount", "TenCentCount", "TwentyDollarCount" },
                values: new object[] { 1L, 10, 10, 10, 10, 10, 10 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SnackMachine");
        }
    }
}
