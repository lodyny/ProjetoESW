using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdotAqui.Migrations
{
    public partial class Interventions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalIntervention",
                columns: table => new
                {
                    InterventionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnimalId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnimalIntervention_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalIntervention_AnimalId",
                table: "AnimalIntervention",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalIntervention_UserId",
                table: "AnimalIntervention",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalIntervention");
        }
    }
}
