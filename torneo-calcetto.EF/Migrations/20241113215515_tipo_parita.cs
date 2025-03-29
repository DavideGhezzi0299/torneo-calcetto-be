using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace torneo_calcetto.EF.Migrations
{
    /// <inheritdoc />
    public partial class tipo_parita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoPartita",
                table: "Partite",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoPartita",
                table: "Partite");
        }
    }
}
