using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace torneo_calcetto.EF.Migrations
{
    /// <inheritdoc />
    public partial class add_fascia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fascia",
                table: "Partite");

            migrationBuilder.AddColumn<int>(
                name: "Fascia",
                table: "Squadre",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fascia",
                table: "Squadre");

            migrationBuilder.AddColumn<int>(
                name: "Fascia",
                table: "Partite",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
