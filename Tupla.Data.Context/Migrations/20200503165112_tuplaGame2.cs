using Microsoft.EntityFrameworkCore.Migrations;

namespace Tupla.Data.Context.Migrations
{
    public partial class tuplaGame2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Company_CompanyID",
                table: "Game");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyID",
                table: "Game",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Company_CompanyID",
                table: "Game",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "companyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Company_CompanyID",
                table: "Game");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyID",
                table: "Game",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Company_CompanyID",
                table: "Game",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "companyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
