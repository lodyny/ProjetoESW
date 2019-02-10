using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdotAqui.Migrations
{
    public partial class Interventions3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "AnimalIntervention",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Date",
                table: "AnimalIntervention",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
