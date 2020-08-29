using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace itec_mobile_api_final.Migrations
{
    public partial class temperatureMeasurementFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemperatureMeasurementEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    HouseTemperature = table.Column<int>(nullable: false),
                    HeaterTemperature = table.Column<int>(nullable: false),
                    OutsideTemperature = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    MeasurementTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureMeasurementEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemperatureMeasurementEntities");
        }
    }
}
