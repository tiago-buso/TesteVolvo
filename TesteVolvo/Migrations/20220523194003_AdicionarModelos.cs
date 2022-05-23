using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteVolvo.Migrations
{
    public partial class AdicionarModelos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[TruckModel] ([Description],[YearOfModel]) VALUES ('FH',2022)");
            migrationBuilder.Sql(@"INSERT INTO [dbo].[TruckModel] ([Description],[YearOfModel]) VALUES ('FM' ,2022)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
