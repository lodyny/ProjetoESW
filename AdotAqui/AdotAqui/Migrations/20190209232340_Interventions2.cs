using Microsoft.EntityFrameworkCore.Migrations;

namespace AdotAqui.Migrations
{
    public partial class Interventions2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalIntervention_Animals_AnimalId",
                table: "AnimalIntervention");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalIntervention_Users_UserId",
                table: "AnimalIntervention");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AnimalIntervention",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "AnimalIntervention",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalIntervention_Animals_AnimalId",
                table: "AnimalIntervention",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "AnimalID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalIntervention_Users_UserId",
                table: "AnimalIntervention",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalIntervention_Animals_AnimalId",
                table: "AnimalIntervention");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalIntervention_Users_UserId",
                table: "AnimalIntervention");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AnimalIntervention",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "AnimalIntervention",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalIntervention_Animals_AnimalId",
                table: "AnimalIntervention",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "AnimalID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalIntervention_Users_UserId",
                table: "AnimalIntervention",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
