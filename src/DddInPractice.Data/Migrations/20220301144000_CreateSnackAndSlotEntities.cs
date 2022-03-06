using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DddInPractice.Data.Migrations
{
    public partial class CreateSnackAndSlotEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Snack",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snack", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slot",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(type: "int", nullable: false),
                    SnackId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    SnackMachineId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slot_Snack_SnackId",
                        column: x => x.SnackId,
                        principalTable: "Snack",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slot_SnackMachine_SnackMachineId",
                        column: x => x.SnackMachineId,
                        principalTable: "SnackMachine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Snack",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1L, "Chocolate" });

            migrationBuilder.InsertData(
                table: "Snack",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2L, "Soda" });

            migrationBuilder.InsertData(
                table: "Snack",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3L, "Gum" });

            migrationBuilder.InsertData(
                table: "Slot",
                columns: new[] { "Id", "Position", "SnackMachineId", "Price", "Quantity", "SnackId" },
                values: new object[] { 1L, 1, 1L, 3m, 10, 1L });

            migrationBuilder.InsertData(
                table: "Slot",
                columns: new[] { "Id", "Position", "SnackMachineId", "Price", "Quantity", "SnackId" },
                values: new object[] { 2L, 2, 1L, 2m, 15, 2L });

            migrationBuilder.InsertData(
                table: "Slot",
                columns: new[] { "Id", "Position", "SnackMachineId", "Price", "Quantity", "SnackId" },
                values: new object[] { 3L, 3, 1L, 1m, 20, 3L });

            migrationBuilder.CreateIndex(
                name: "IX_Slot_SnackId",
                table: "Slot",
                column: "SnackId");

            migrationBuilder.CreateIndex(
                name: "IX_Slot_SnackMachineId",
                table: "Slot",
                column: "SnackMachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slot");

            migrationBuilder.DropTable(
                name: "Snack");
        }
    }
}
