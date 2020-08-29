using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace itec_mobile_api_final.Migrations
{
    public partial class HeaterSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeaterScheduleEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    HeaterStartTime = table.Column<DateTime>(nullable: false),
                    HeaterFinishedTime = table.Column<DateTime>(nullable: false),
                    InitialHouseTemperature = table.Column<float>(nullable: false),
                    InitialOutsideTemperature = table.Column<float>(nullable: false),
                    FinalOutsideTemperature = table.Column<float>(nullable: false),
                    FinalHouseTemperature = table.Column<float>(nullable: false),
                    HeaterAverageTemperature = table.Column<float>(nullable: false),
                    OutsideAverageTemperature = table.Column<float>(nullable: false),
                    HeatingTime = table.Column<float>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaterScheduleEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeaterScheduleEntities");
        }
    }
}
