using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tupla.Data.Context.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //game and modified customers, also picture
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
            //platform and tag
            migrationBuilder.CreateTable(
                    name: "Platform",
                    columns: table => new
                    {
                        PlatformId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Platform_name = table.Column<string>(nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Platform", x => x.PlatformId);
                    });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_Tag_Customers_UserName",
                        column: x => x.Username,
                        principalTable: "Customers",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlatformOfGame",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    PlatformId = table.Column<int>(nullable: false),
                    Authentication = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformOfGame", x => new { x.GameId, x.PlatformId });
                    table.ForeignKey(
                        name: "FK_PlatformOfGame_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatformOfGame_Platform_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platform",
                        principalColumn: "PlatformId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameTag",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTag", x => new { x.GameId, x.TagId, x.Username });
                    table.ForeignKey(
                        name: "FK_GameTag_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameTag_Customers_UserName",
                        column: x => x.Username,
                        principalTable: "Customers",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.NoAction);
                });

            //WishList and Credit card
            migrationBuilder.CreateTable(
                    name: "CreditCard",
                    columns: table => new
                    {
                        CreditId = table.Column<string>(nullable: false),
                        Username = table.Column<string>(maxLength: 256, nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_CreditCard", x => x.CreditId);
                        table.ForeignKey(
                            name: "FK_CreditCard_Customers_UserName",
                            column: x => x.Username,
                            principalTable: "Customers",
                            principalColumn: "UserName",
                            onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateTable(
                name: "WishList",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishList", x => new { x.GameId, x.Username });
                    table.ForeignKey(
                        name: "FK_WishList_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishList_Customers_UserName",
                        column: x => x.Username,
                        principalTable: "Customers",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            //Shopping update
            migrationBuilder.CreateTable(
                    name: "Cart",
                    columns: table => new
                    {
                        GameId = table.Column<int>(nullable: false),
                        PlatformId = table.Column<int>(nullable: false),
                        CartId = table.Column<string>(maxLength: 256, nullable: true),
                        Quantity = table.Column<int>(nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Cart", x => new { x.GameId, x.PlatformId });
                        table.ForeignKey(
                            name: "FK_Cart_PlatformOfGame_GameId_PlatformId",
                            columns: x => new { x.GameId, x.PlatformId },
                            principalTable: "PlatformOfGame",
                            principalColumns: new[] { "GameId", "PlatformId" },
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                                name: "FK_Cart_Customers_UserName",
                                column: x => x.CartId,
                                principalTable: "Customers",
                                principalColumn: "UserName",
                                onDelete: ReferentialAction.Cascade);
                    });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 256, nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Transaction_Customers_UserName",
                        column: x => x.Username,
                        principalTable: "Customers",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    PlatformId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => new { x.OrderId, x.GameId, x.PlatformId });
                    table.ForeignKey(
                        name: "FK_OrderDetail_Transaction_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Transaction",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_PlatformOfGame_GameId_PlatformId",
                        columns: x => new { x.GameId, x.PlatformId },
                        principalTable: "PlatformOfGame",
                        principalColumns: new[] { "GameId", "PlatformId" },
                        onDelete: ReferentialAction.NoAction);
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

            migrationBuilder.CreateIndex(
                name: "IX_GameTag_TagId",
                table: "GameTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTag_Username",
                table: "GameTag",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformOfGame_PlatformId",
                table: "PlatformOfGame",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Username",
                table: "Tag",
                column: "Username");
            migrationBuilder.CreateIndex(
                    name: "IX_CreditCard_Username",
                    table: "CreditCard",
                    column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_Username",
                table: "WishList",
                column: "Username");

            migrationBuilder.CreateIndex(
                    name: "IX_OrderDetail_GameId_PlatformId",
                    table: "OrderDetail",
                    columns: new[] { "GameId", "PlatformId" });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Username",
                table: "Transaction",
                column: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "CompanyPicture");

            migrationBuilder.DropTable(
                name: "CreditCard");

            migrationBuilder.DropTable(
                name: "CustomerPicture");

            migrationBuilder.DropTable(
                name: "GamePicture");

            migrationBuilder.DropTable(
                name: "GameTag");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "WishList");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "PlatformOfGame");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Platform");
        }
    }
}
