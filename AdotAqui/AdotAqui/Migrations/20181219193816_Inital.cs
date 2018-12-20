using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdotAqui.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Birthday",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AnimalSpecies",
                columns: table => new
                {
                    SpecieID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Name_PT = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalSpecies", x => x.SpecieID);
                });

            migrationBuilder.CreateTable(
                name: "AnimalBreeds",
                columns: table => new
                {
                    BreedID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Name_PT = table.Column<string>(maxLength: 50, nullable: false),
                    SpecieID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalBreeds", x => x.BreedID);
                    table.ForeignKey(
                        name: "FK_AnimalBreeds_AnimalBreeds",
                        column: x => x.SpecieID,
                        principalTable: "AnimalSpecies",
                        principalColumn: "SpecieID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    AnimalID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    UserID = table.Column<int>(nullable: true),
                    Gender = table.Column<string>(unicode: false, maxLength: 1, nullable: false),
                    BreedID = table.Column<int>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    Birthday = table.Column<DateTime>(type: "date", nullable: true),
                    Details = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.AnimalID);
                    table.ForeignKey(
                        name: "FK_Animals_AnimalBreeds",
                        column: x => x.BreedID,
                        principalTable: "AnimalBreeds",
                        principalColumn: "BreedID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalBreeds_SpecieID",
                table: "AnimalBreeds",
                column: "SpecieID");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_BreedID",
                table: "Animals",
                column: "BreedID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "AnimalBreeds");

            migrationBuilder.DropTable(
                name: "AnimalSpecies");

            migrationBuilder.AlterColumn<string>(
                name: "Birthday",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
