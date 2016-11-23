using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pagae.Data.Migrations
{
    public partial class Punishment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Punishment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    PunishedId = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    InFavor = table.Column<bool>(nullable: false),
                    PunishmentId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK", x => x.Id);
                //     table.ForeignKey(
                //         name: "FK_Vote_Punishment_PunishmentId",
                //         column: x => x.PunishmentId,
                //         principalTable: "Punishment",
                //         principalColumn: "Id",
                //         onDelete: ReferentialAction.Restrict);
                //     table.ForeignKey(
                //         name: "FK_Vote_AspNetUsers_UserId",
                //         column: x => x.UserId,
                //         principalTable: "AspNetUsers",
                //         principalColumn: "Id",
                //         onDelete: ReferentialAction.Restrict);
                });

            // migrationBuilder.CreateIndex(
            //     name: "IX_Vote_PunishmentId",
            //     table: "Vote",
            //     column: "PunishmentId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_Vote_UserId",
            //     table: "Vote",
            //     column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "Punishment");
        }
    }
}
