using Microsoft.EntityFrameworkCore.Migrations;

namespace itec_mobile_api_final.Migrations
{
    public partial class addedUserInHeater : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "HeaterEntities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HeaterEntities_UserId",
                table: "HeaterEntities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HeaterEntities_AspNetUsers_UserId",
                table: "HeaterEntities",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeaterEntities_AspNetUsers_UserId",
                table: "HeaterEntities");

            migrationBuilder.DropIndex(
                name: "IX_HeaterEntities_UserId",
                table: "HeaterEntities");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HeaterEntities");
        }
    }
}
