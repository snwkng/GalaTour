using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GalaTour.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExCities",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExCities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExDates",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExDates", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExDurations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Duration = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExDurations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExHotels",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HotelName = table.Column<string>(nullable: true),
                    HotelLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExHotels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExImages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExImages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ExPrices",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExPrices", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ThePriceIncludes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PriceInclude = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThePriceIncludes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Excursions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ThePriceIncludeID = table.Column<int>(nullable: false),
                    ExImageID = table.Column<int>(nullable: false),
                    ExDurationID = table.Column<int>(nullable: false),
                    ExCityID = table.Column<int>(nullable: false),
                    ExDateID = table.Column<int>(nullable: false),
                    ExPriceID = table.Column<int>(nullable: false),
                    ExHotelID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excursions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Excursions_ExCities_ExCityID",
                        column: x => x.ExCityID,
                        principalTable: "ExCities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Excursions_ExDates_ExDateID",
                        column: x => x.ExDateID,
                        principalTable: "ExDates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Excursions_ExDurations_ExDurationID",
                        column: x => x.ExDurationID,
                        principalTable: "ExDurations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Excursions_ExHotels_ExHotelID",
                        column: x => x.ExHotelID,
                        principalTable: "ExHotels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Excursions_ExImages_ExImageID",
                        column: x => x.ExImageID,
                        principalTable: "ExImages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Excursions_ExPrices_ExPriceID",
                        column: x => x.ExPriceID,
                        principalTable: "ExPrices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Excursions_ThePriceIncludes_ThePriceIncludeID",
                        column: x => x.ThePriceIncludeID,
                        principalTable: "ThePriceIncludes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_ExCityID",
                table: "Excursions",
                column: "ExCityID");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_ExDateID",
                table: "Excursions",
                column: "ExDateID");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_ExDurationID",
                table: "Excursions",
                column: "ExDurationID");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_ExHotelID",
                table: "Excursions",
                column: "ExHotelID");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_ExImageID",
                table: "Excursions",
                column: "ExImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_ExPriceID",
                table: "Excursions",
                column: "ExPriceID");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_ThePriceIncludeID",
                table: "Excursions",
                column: "ThePriceIncludeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Excursions");

            migrationBuilder.DropTable(
                name: "ExCities");

            migrationBuilder.DropTable(
                name: "ExDates");

            migrationBuilder.DropTable(
                name: "ExDurations");

            migrationBuilder.DropTable(
                name: "ExHotels");

            migrationBuilder.DropTable(
                name: "ExImages");

            migrationBuilder.DropTable(
                name: "ExPrices");

            migrationBuilder.DropTable(
                name: "ThePriceIncludes");
        }
    }
}
