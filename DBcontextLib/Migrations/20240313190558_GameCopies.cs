﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBcontextLib.Migrations
{
    /// <inheritdoc />
    public partial class GameCopies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Copies",
                table: "Games",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Copies",
                table: "Games");
        }
    }
}
