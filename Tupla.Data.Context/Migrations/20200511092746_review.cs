using Microsoft.EntityFrameworkCore.Migrations;

namespace Tupla.Data.Context.Migrations
{
    public partial class review : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    PlatformId = table.Column<int>(nullable: false),
                    Recommended = table.Column<bool>(nullable: false),
                    Review_Detail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => new { x.OrderId, x.GameId, x.PlatformId });
                    table.ForeignKey(
                        name: "FK_Review_OrderDetail_OrderId_GameId_PlatformId",
                        columns: x => new { x.OrderId, x.GameId, x.PlatformId },
                        principalTable: "OrderDetail",
                        principalColumns: new[] { "OrderId", "GameId", "PlatformId" },
                        onDelete: ReferentialAction.NoAction);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");
        }
    }
}
