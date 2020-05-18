using Microsoft.EntityFrameworkCore.Migrations;

namespace Tupla.Data.Context.Migrations
{
    public partial class code : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Code",
                columns: table => new
                {
                    CodeId = table.Column<string>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    PlatformId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Code", x => x.CodeId);
                    table.ForeignKey(
                        name: "FK_Code_OrderDetail_OrderId_GameId_PlatformId",
                        columns: x => new { x.OrderId, x.GameId, x.PlatformId },
                        principalTable: "OrderDetail",
                        principalColumns: new[] { "OrderId", "GameId", "PlatformId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Code_OrderId_GameId_PlatformId",
                table: "Code",
                columns: new[] { "OrderId", "GameId", "PlatformId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Code");
        }
    }
}
