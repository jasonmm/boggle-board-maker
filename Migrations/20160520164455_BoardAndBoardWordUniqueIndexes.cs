using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoggleBoardMaker.Migrations
{
    public partial class BoardAndBoardWordUniqueIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BoardWords_BoardId_Word",
                table: "BoardWords",
                columns: new[] { "BoardId", "Word" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_BoardStr",
                table: "Boards",
                column: "BoardStr",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BoardWords_BoardId_Word",
                table: "BoardWords");

            migrationBuilder.DropIndex(
                name: "IX_Boards_BoardStr",
                table: "Boards");
        }
    }
}
