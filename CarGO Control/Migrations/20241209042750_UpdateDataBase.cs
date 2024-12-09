using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarGO_Control.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Drivers_DriverID",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_IDTruck",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_TruckID",
                table: "Drivers");

            migrationBuilder.RenameColumn(
                name: "DriverID",
                table: "Routes",
                newName: "DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_DriverID",
                table: "Routes",
                newName: "IX_Routes_DriverId");

            migrationBuilder.AddColumn<int>(
                name: "RouteID",
                table: "Trucks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DriverID",
                table: "Routes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TruckID1",
                table: "Drivers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_RouteID",
                table: "Trucks",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_DriverID",
                table: "Routes",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_IDTruck",
                table: "Routes",
                column: "IDTruck");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_TruckID",
                table: "Drivers",
                column: "TruckID");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_TruckID1",
                table: "Drivers",
                column: "TruckID1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Trucks_TruckID1",
                table: "Drivers",
                column: "TruckID1",
                principalTable: "Trucks",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Drivers_DriverID",
                table: "Routes",
                column: "DriverID",
                principalTable: "Drivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Drivers_DriverId",
                table: "Routes",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_Routes_RouteID",
                table: "Trucks",
                column: "RouteID",
                principalTable: "Routes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Trucks_TruckID1",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Drivers_DriverID",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Drivers_DriverId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_Routes_RouteID",
                table: "Trucks");

            migrationBuilder.DropIndex(
                name: "IX_Trucks_RouteID",
                table: "Trucks");

            migrationBuilder.DropIndex(
                name: "IX_Routes_DriverID",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_IDTruck",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_TruckID",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_TruckID1",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "RouteID",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "DriverID",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "TruckID1",
                table: "Drivers");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "Routes",
                newName: "DriverID");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_DriverId",
                table: "Routes",
                newName: "IX_Routes_DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_IDTruck",
                table: "Routes",
                column: "IDTruck",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_TruckID",
                table: "Drivers",
                column: "TruckID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Drivers_DriverID",
                table: "Routes",
                column: "DriverID",
                principalTable: "Drivers",
                principalColumn: "Id");
        }
    }
}
