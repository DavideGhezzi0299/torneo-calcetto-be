using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace torneo_calcetto.EF.Migrations
{
    /// <inheritdoc />
    public partial class utenti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FkIdUtenteCreatore",
                table: "Tornei",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UtenteNavigationIdPk",
                table: "Tornei",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    IdPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.IdPk);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tornei_UtenteNavigationIdPk",
                table: "Tornei",
                column: "UtenteNavigationIdPk");

            migrationBuilder.AddForeignKey(
                name: "FK_Tornei_Utenti_UtenteNavigationIdPk",
                table: "Tornei",
                column: "UtenteNavigationIdPk",
                principalTable: "Utenti",
                principalColumn: "IdPk",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tornei_Utenti_UtenteNavigationIdPk",
                table: "Tornei");

            migrationBuilder.DropTable(
                name: "Utenti");

            migrationBuilder.DropIndex(
                name: "IX_Tornei_UtenteNavigationIdPk",
                table: "Tornei");

            migrationBuilder.DropColumn(
                name: "FkIdUtenteCreatore",
                table: "Tornei");

            migrationBuilder.DropColumn(
                name: "UtenteNavigationIdPk",
                table: "Tornei");
        }
    }
}
