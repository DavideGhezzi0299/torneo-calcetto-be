using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace torneo_calcetto.EF.Migrations
{
    /// <inheritdoc />
    public partial class first_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partite",
                columns: table => new
                {
                    IdPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SquadraCasa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SquadraOspite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GolCasa = table.Column<int>(type: "int", nullable: false),
                    GolOspite = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vincitore = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partite", x => x.IdPk);
                });

            migrationBuilder.CreateTable(
                name: "Squadre",
                columns: table => new
                {
                    IdPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PuntiFatti = table.Column<int>(type: "int", nullable: false),
                    GolFatti = table.Column<int>(type: "int", nullable: false),
                    GolSubiti = table.Column<int>(type: "int", nullable: false),
                    PartiteGiocate = table.Column<int>(type: "int", nullable: false),
                    Vittorie = table.Column<int>(type: "int", nullable: false),
                    Sconfitte = table.Column<int>(type: "int", nullable: false),
                    Pareggi = table.Column<int>(type: "int", nullable: false),
                    DifferenzaReti = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Squadre", x => x.IdPk);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partite");

            migrationBuilder.DropTable(
                name: "Squadre");
        }
    }
}
