using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tupla.Data.Context.Migrations
{
    public partial class promotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventPromotion",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Event_name = table.Column<string>(maxLength: 100, nullable: false),
                    Event_start_date = table.Column<DateTime>(nullable: false),
                    Event_end_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPromotion", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    PromotionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(nullable: false),
                    PlatformId = table.Column<int>(nullable: false),
                    Percentage = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.PromotionId);
                    table.ForeignKey(
                        name: "FK_Promotion_EventPromotion_EventId",
                        column: x => x.EventId,
                        principalTable: "EventPromotion",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Promotion_PlatformOfGame_GameId_PlatformId",
                        columns: x => new { x.GameId, x.PlatformId },
                        principalTable: "PlatformOfGame",
                        principalColumns: new[] { "GameId", "PlatformId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_EventId",
                table: "Promotion",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_GameId_PlatformId",
                table: "Promotion",
                columns: new[] { "GameId", "PlatformId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "EventPromotion");
        }
    }
}
