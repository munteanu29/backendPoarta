using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace itec_mobile_api_final.Migrations
{
    public partial class removedHeaterStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeaterEntities");

            migrationBuilder.DropTable(
                name: "HeaterScheduleEntities");

            migrationBuilder.DropTable(
                name: "TemperatureMeasurementEntities");

            migrationBuilder.CreateTable(
                name: "DoorEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    IsOpen = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoorEntities");

            migrationBuilder.CreateTable(
                name: "HeaterEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    IsOn = table.Column<bool>(nullable: false),
                    SetTemperature = table.Column<int>(nullable: false),
                    Temperature = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaterEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeaterEntities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HeaterScheduleEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    FinalHouseTemperature = table.Column<float>(nullable: false),
                    FinalOutsideTemperature = table.Column<float>(nullable: false),
                    HeaterAverageTemperature = table.Column<float>(nullable: false),
                    HeaterFinishedTime = table.Column<DateTime>(nullable: false),
                    HeaterStartTime = table.Column<DateTime>(nullable: false),
                    HeatingTime = table.Column<float>(nullable: false),
                    InitialHouseTemperature = table.Column<float>(nullable: false),
                    InitialOutsideTemperature = table.Column<float>(nullable: false),
                    OutsideAverageTemperature = table.Column<float>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaterScheduleEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureMeasurementEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    HeaterTemperature = table.Column<int>(nullable: false),
                    HouseTemperature = table.Column<int>(nullable: false),
                    MeasurementTime = table.Column<DateTime>(nullable: false),
                    OutsideTemperature = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureMeasurementEntities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeaterEntities_UserId",
                table: "HeaterEntities",
                column: "UserId");
        }
    }
}
