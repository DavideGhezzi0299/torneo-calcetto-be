using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace torneo_calcetto.EF.Migrations
{
    /// <inheritdoc />
    public partial class gironi2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partite_Tornei_TorneoNavigationIdPk",
                table: "Partite");

            migrationBuilder.DropColumn(
                name: "FkIdTorneo",
                table: "Partite");

            migrationBuilder.RenameColumn(
                name: "TorneoNavigationIdPk",
                table: "Partite",
                newName: "FkIdGirone");

            migrationBuilder.RenameIndex(
                name: "IX_Partite_TorneoNavigationIdPk",
                table: "Partite",
                newName: "IX_Partite_FkIdGirone");

            migrationBuilder.AddColumn<int>(
                name: "TipoTorneo",
                table: "Tornei",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Gironi_FkIdTorneo",
                table: "Gironi",
                column: "FkIdTorneo");

            migrationBuilder.AddForeignKey(
                name: "FK_Gironi_Tornei_FkIdTorneo",
                table: "Gironi",
                column: "FkIdTorneo",
                principalTable: "Tornei",
                principalColumn: "IdPk",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partite_Gironi_FkIdGirone",
                table: "Partite",
                column: "FkIdGirone",
                principalTable: "Gironi",
                principalColumn: "IdPk",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gironi_Tornei_FkIdTorneo",
                table: "Gironi");

            migrationBuilder.DropForeignKey(
                name: "FK_Partite_Gironi_FkIdGirone",
                table: "Partite");

            migrationBuilder.DropIndex(
                name: "IX_Gironi_FkIdTorneo",
                table: "Gironi");

            migrationBuilder.DropColumn(
                name: "TipoTorneo",
                table: "Tornei");

            migrationBuilder.RenameColumn(
                name: "FkIdGirone",
                table: "Partite",
                newName: "TorneoNavigationIdPk");

            migrationBuilder.RenameIndex(
                name: "IX_Partite_FkIdGirone",
                table: "Partite",
                newName: "IX_Partite_TorneoNavigationIdPk");

            migrationBuilder.AddColumn<int>(
                name: "FkIdTorneo",
                table: "Partite",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Partite_Tornei_TorneoNavigationIdPk",
                table: "Partite",
                column: "TorneoNavigationIdPk",
                principalTable: "Tornei",
                principalColumn: "IdPk",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
