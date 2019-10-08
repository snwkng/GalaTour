using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GalaTour.Migrations
{
    public partial class CreateNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_ExDescriptions_ExDescriptionID",
                table: "Excursions");

            migrationBuilder.DropTable(
                name: "ExDescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Excursions_ExDescriptionID",
                table: "Excursions");

            migrationBuilder.DropColumn(
                name: "ExDescriptionID",
                table: "Excursions");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Excursions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Excursions");

            migrationBuilder.AddColumn<int>(
                name: "ExDescriptionID",
                table: "Excursions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExDescriptions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Day1 = table.Column<string>(nullable: true),
                    Day10 = table.Column<string>(nullable: true),
                    Day2 = table.Column<string>(nullable: true),
                    Day3 = table.Column<string>(nullable: true),
                    Day4 = table.Column<string>(nullable: true),
                    Day5 = table.Column<string>(nullable: true),
                    Day7 = table.Column<string>(nullable: true),
                    Day8 = table.Column<string>(nullable: true),
                    Day9 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExDescriptions", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_ExDescriptionID",
                table: "Excursions",
                column: "ExDescriptionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_ExDescriptions_ExDescriptionID",
                table: "Excursions",
                column: "ExDescriptionID",
                principalTable: "ExDescriptions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
