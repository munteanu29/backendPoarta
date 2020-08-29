using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace itec_mobile_api_final.Migrations
{
    public partial class deletedItec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarEntities");

            migrationBuilder.DropTable(
                name: "MessageEntities");

            migrationBuilder.DropTable(
                name: "VoteEntities");

            migrationBuilder.DropTable(
                name: "TopicEntities");

            migrationBuilder.DropTable(
                name: "StationEntities");

            migrationBuilder.DropTable(
                name: "CategoryEntities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Autonomy = table.Column<float>(nullable: false),
                    BatteryLeft = table.Column<float>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    LastTechRevision = table.Column<DateTime>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarEntities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastEdited = table.Column<DateTime>(nullable: false),
                    ParentId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryEntities_CategoryEntities_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CategoryEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryEntities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StationEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    FreeSockets = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Old = table.Column<bool>(nullable: false),
                    OldStationId = table.Column<string>(nullable: true),
                    TotalSockets = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StationEntities_StationEntities_OldStationId",
                        column: x => x.OldStationId,
                        principalTable: "StationEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StationEntities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TopicEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CategoryId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastEdited = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicEntities_CategoryEntities_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopicEntities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VoteEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    StationId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Vote = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteEntities_StationEntities_StationId",
                        column: x => x.StationId,
                        principalTable: "StationEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoteEntities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastEdited = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    TopicId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageEntities_TopicEntities_TopicId",
                        column: x => x.TopicId,
                        principalTable: "TopicEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageEntities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarEntities_UserId",
                table: "CarEntities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEntities_ParentId",
                table: "CategoryEntities",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEntities_UserId",
                table: "CategoryEntities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageEntities_TopicId",
                table: "MessageEntities",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageEntities_UserId",
                table: "MessageEntities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StationEntities_OldStationId",
                table: "StationEntities",
                column: "OldStationId");

            migrationBuilder.CreateIndex(
                name: "IX_StationEntities_UserId",
                table: "StationEntities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicEntities_CategoryId",
                table: "TopicEntities",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicEntities_UserId",
                table: "TopicEntities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteEntities_StationId",
                table: "VoteEntities",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteEntities_UserId",
                table: "VoteEntities",
                column: "UserId");
        }
    }
}
