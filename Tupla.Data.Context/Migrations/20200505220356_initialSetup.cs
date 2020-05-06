using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tupla.Data.Context.Migrations
{
    public partial class initialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "IX_UserName_Unique_Constraint",
                table: "Customers",
                column: "UserName"
                );

            migrationBuilder.CreateTable(
                name: "CompanyPicture",
                columns: table => new
                {
                    Path = table.Column<string>(nullable: false),
                    imageType = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPicture", x => x.Path);
                    table.ForeignKey(
                        name: "FK_CompanyPicture_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "companyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameName = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    HtmlText = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Game_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "companyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPicture",
                columns: table => new
                {
                    Path = table.Column<string>(nullable: false),
                    imageType = table.Column<int>(nullable: false),
                    Username = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPicture", x => x.Path);
                    table.ForeignKey(
                        name: "FK_CustomerPicture_Customers_UserName",
                        column: x => x.Username,
                        principalTable: "Customers",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePicture",
                columns: table => new
                {
                    Path = table.Column<string>(nullable: false),
                    imageType = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePicture", x => x.Path);
                    table.ForeignKey(
                        name: "FK_GamePicture_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPicture_CompanyId",
                table: "CompanyPicture",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPicture_Username",
                table: "CustomerPicture",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_Game_CompanyID",
                table: "Game",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_GamePicture_GameId",
                table: "GamePicture",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyPicture");

            migrationBuilder.DropTable(
                name: "CustomerPicture");

            migrationBuilder.DropTable(
                name: "GamePicture");

            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}
