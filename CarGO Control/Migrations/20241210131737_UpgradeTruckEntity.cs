using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarGO_Control.Migrations
{
    /// <inheritdoc />
    public partial class UpgradeTruckEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InWay",
                table: "Trucks",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InWay",
                table: "Trucks");
        }
    }
}
