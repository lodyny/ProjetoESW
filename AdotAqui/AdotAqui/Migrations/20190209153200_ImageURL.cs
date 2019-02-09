using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdotAqui.Migrations
{
    public partial class ImageURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnimalIntervention",
                columns: table => new
                {
                    InterventionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnimalId = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    Date = table.Column<int>(nullable: false),
                    Completed = table.Column<bool>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalIntervention", x => x.InterventionId);
                    table.ForeignKey(
                        name: "FK_AnimalIntervention_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalIntervention_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalIntervention_AnimalId",
                table: "AnimalIntervention",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalIntervention_UserID",
                table: "AnimalIntervention",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalIntervention");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Users");
        }
    }
}
