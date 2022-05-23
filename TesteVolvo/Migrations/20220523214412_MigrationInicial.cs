using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteVolvo.Migrations
{
    public partial class MigrationInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseTruckModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseTruckModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TruckModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseTruckModelId = table.Column<int>(type: "int", nullable: false),
                    YearOfModel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TruckModel_BaseTruckModel_BaseTruckModelId",
                        column: x => x.BaseTruckModelId,
                        principalTable: "BaseTruckModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Truck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruckModelId = table.Column<int>(type: "int", nullable: false),
                    YearOfManufacture = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Truck_TruckModel_TruckModelId",
                        column: x => x.TruckModelId,
                        principalTable: "TruckModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Truck_TruckModelId",
                table: "Truck",
                column: "TruckModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TruckModel_BaseTruckModelId",
                table: "TruckModel",
                column: "BaseTruckModelId");

            migrationBuilder.Sql(@"INSERT INTO [dbo].[BaseTruckModel] ([Description]) VALUES ('FH')");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[BaseTruckModel] ([Description]) VALUES ('FM')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Truck");

            migrationBuilder.DropTable(
                name: "TruckModel");

            migrationBuilder.DropTable(
                name: "BaseTruckModel");
        }
    }
}
