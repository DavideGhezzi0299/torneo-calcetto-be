using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace torneo_calcetto.EF.Migrations
{
    /// <inheritdoc />
    public partial class gironi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FkIdGirone",
                table: "Squadre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Gironi",
                columns: table => new
                {
                    IdPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkIdTorneo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gironi", x => x.IdPk);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Squadre_FkIdGirone",
                table: "Squadre",
                column: "FkIdGirone");

            migrationBuilder.AddForeignKey(
                name: "FK_Squadre_Gironi_FkIdGirone",
                table: "Squadre",
                column: "FkIdGirone",
                principalTable: "Gironi",
                principalColumn: "IdPk",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Squadre_Gironi_FkIdGirone",
                table: "Squadre");

            migrationBuilder.DropTable(
                name: "Gironi");

            migrationBuilder.DropIndex(
                name: "IX_Squadre_FkIdGirone",
                table: "Squadre");

            migrationBuilder.DropColumn(
                name: "FkIdGirone",
                table: "Squadre");
        }
    }
}
