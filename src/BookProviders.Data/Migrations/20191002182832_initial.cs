using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookProviders.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caterers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Document = table.Column<string>(type: "varchar(14)", nullable: false),
                    CatererType = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caterers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CatererId = table.Column<Guid>(nullable: false),
                    Street = table.Column<string>(type: "varchar(400)", nullable: false),
                    Number = table.Column<string>(type: "varchar(50)", nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(8)", nullable: false),
                    City = table.Column<string>(type: "varchar(100)", nullable: false),
                    State = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Caterers_CatererId",
                        column: x => x.CatererId,
                        principalTable: "Caterers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CatererId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Image = table.Column<string>(type: "varchar(100)", nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Caterers_CatererId",
                        column: x => x.CatererId,
                        principalTable: "Caterers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CatererId",
                table: "Addresses",
                column: "CatererId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CatererId",
                table: "Products",
                column: "CatererId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Caterers");
        }
    }
}
