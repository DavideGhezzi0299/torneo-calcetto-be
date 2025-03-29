using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace torneo_calcetto.EF.Migrations
{
    /// <inheritdoc />
    public partial class modifiche : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FkIdSquadraCasa",
                table: "Partite",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FkIdTorneo",
                table: "Partite",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FkIdTrasferta",
                table: "Partite",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TorneoNavigationIdPk",
                table: "Partite",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tornei",
                columns: table => new
                {
                    IdPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tornei", x => x.IdPk);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partite_FkIdSquadraCasa",
                table: "Partite",
                column: "FkIdSquadraCasa");

            migrationBuilder.CreateIndex(
                name: "IX_Partite_FkIdTrasferta",
                table: "Partite",
                column: "FkIdTrasferta");

            migrationBuilder.CreateIndex(
                name: "IX_Partite_TorneoNavigationIdPk",
                table: "Partite",
                column: "TorneoNavigationIdPk");

            migrationBuilder.AddForeignKey(
                name: "FK_Partite_Squadre_FkIdSquadraCasa",
                table: "Partite",
                column: "FkIdSquadraCasa",
                principalTable: "Squadre",
                principalColumn: "IdPk",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partite_Squadre_FkIdTrasferta",
                table: "Partite",
                column: "FkIdTrasferta",
                principalTable: "Squadre",
                principalColumn: "IdPk",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partite_Tornei_TorneoNavigationIdPk",
                table: "Partite",
                column: "TorneoNavigationIdPk",
                principalTable: "Tornei",
                principalColumn: "IdPk",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partite_Squadre_FkIdSquadraCasa",
                table: "Partite");

            migrationBuilder.DropForeignKey(
                name: "FK_Partite_Squadre_FkIdTrasferta",
                table: "Partite");

            migrationBuilder.DropForeignKey(
                name: "FK_Partite_Tornei_TorneoNavigationIdPk",
                table: "Partite");

            migrationBuilder.DropTable(
                name: "Tornei");

            migrationBuilder.DropIndex(
                name: "IX_Partite_FkIdSquadraCasa",
                table: "Partite");

            migrationBuilder.DropIndex(
                name: "IX_Partite_FkIdTrasferta",
                table: "Partite");

            migrationBuilder.DropIndex(
                name: "IX_Partite_TorneoNavigationIdPk",
                table: "Partite");

            migrationBuilder.DropColumn(
                name: "FkIdSquadraCasa",
                table: "Partite");

            migrationBuilder.DropColumn(
                name: "FkIdTorneo",
                table: "Partite");

            migrationBuilder.DropColumn(
                name: "FkIdTrasferta",
                table: "Partite");

            migrationBuilder.DropColumn(
                name: "TorneoNavigationIdPk",
                table: "Partite");
        }
    }
}
