using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProudTech.Migrations
{
    public partial class trilha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrilhaId",
                table: "Participante");

            migrationBuilder.AddColumn<bool>(
                name: "Fechada",
                table: "Trilha",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fechada",
                table: "Trilha");

            migrationBuilder.AddColumn<Guid>(
                name: "TrilhaId",
                table: "Participante",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
