using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace torneo_calcetto.EF.Migrations
{
    /// <inheritdoc />
    public partial class flag_partita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FaseEliminatoria",
                table: "Partite",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaseEliminatoria",
                table: "Partite");
        }
    }
}
